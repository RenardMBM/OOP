from django.http import Http404

from forum.models import *

__all__ = ["filter_articles", "save_article", "save_comment"]


def filter_articles(query: str, page: int, count: int = 5):
    articles = Article.objects.filter(title__icontains=query)
    articles = articles.order_by("-date_created")

    next_article: bool = False
    last_article = (page - 1) * count
    if articles.count() > count + last_article:
        articles = articles[last_article:count + last_article]
        next_article: bool = True

    elif articles.count() > last_article:
        articles = articles[last_article:]

    elif articles.count():
        raise Http404

    return articles, last_article, next_article


def save_article(form, request):
    article: Article = form.save(commit=False)
    article.author = request.user
    article.save()
    form.save_m2m()

    return article


def save_comment(form, article, request):
    comment = form.save(commit=False)
    comment.author = request.user
    comment.parent = article
    comment.save()
    form.save_m2m()

    return comment
