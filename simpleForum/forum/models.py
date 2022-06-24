from django.db import models
from datetime import datetime

from django.contrib.auth import get_user_model

__all__ = ["Comment", "Article"]


class Comment(models.Model):
    author = models.ForeignKey(get_user_model(), on_delete=models.CASCADE)
    parent = models.ForeignKey("Article", on_delete=models.CASCADE)
    content = models.TextField(default="")
    date_created = models.DateTimeField(default=datetime.now)


class Article(models.Model):
    author = models.ForeignKey(get_user_model(), on_delete=models.CASCADE)
    title = models.CharField(max_length=64)
    content = models.TextField(default="")
    date_created = models.DateTimeField(default=datetime.now)
