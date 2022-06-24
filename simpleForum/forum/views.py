from django.shortcuts import render, redirect, get_object_or_404
from django.views import View
from django.http import HttpResponseBadRequest, HttpResponseForbidden

from forum.models import *
from forum.forms import *
from forum.services import *


__all__ = ["IndexView", "ArticleView", "NewArticleView", "CommentsView"]


class IndexView(View):
    @staticmethod
    def get(request):
        try:
            query = request.GET.get("q", "")
            page = int(request.GET.get("page", 1))
        except ValueError:
            return HttpResponseBadRequest()

        articles, last_article, next_article = filter_articles(query, page)
        return render(request, "forum/index.html",
                      {
                          "user": request.user,
                          "articles": articles,
                          "beforeArticle": last_article != 0,
                          "nextArticle": next_article,
                          "isEmpty": not articles.exists(),
                          "query": query,
                          "page": page,
                      })


class NewArticleView(View):
    @staticmethod
    def get(request):
        if request.user.is_authenticated:
            return render(request, "forum/newArticle.html",
                          {"user": request.user,
                           "form": ArticleForm()})

        return redirect("/")

    @staticmethod
    def post(request):
        if not request.user.is_authenticated:
            return HttpResponseForbidden()

        form = ArticleForm(request.POST)
        if form.is_valid():
            article = save_article(form, request)

            return redirect(f"../{article.id}")

        return render(request, "forum/newArticle.html",
                      {"user": request.user,
                       "form": form})


class ArticleView(View):
    @staticmethod
    def get(request, aid):
        article = get_object_or_404(Article, id=aid)

        return render(request, "forum/article.html",
                      {"user": request.user,
                       "article": article,
                       "comments": article.comment_set.order_by("-date_created"),
                       "form": CommentForm()})


class CommentsView(View):
    @staticmethod
    def post(request, aid):
        if not request.user.is_authenticated:
            return HttpResponseForbidden()

        article = get_object_or_404(Article, id=aid)

        form = CommentForm(request.POST)
        if form.is_valid():
            save_comment(form, article, request)

        return redirect("../")
