from django.contrib import admin

from forum.models import *


class CommentInline(admin.TabularInline):
    model = Comment


class ArticleAdmin(admin.ModelAdmin):
    inlines = [
        CommentInline
    ]


admin.site.register(Article, ArticleAdmin)
admin.site.register(Comment)
