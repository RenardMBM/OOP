from django.urls import path
from forum.views import *

urlpatterns = [
    path("", IndexView.as_view()),
    path("article/<int:aid>/", ArticleView.as_view()),
    path("article/<int:aid>/comment/", CommentsView.as_view()),
    path("article/new/", NewArticleView.as_view()),
]
