from django.contrib import admin

from authentication.models import User
from people.admin import ProfileInline


class UserAdmin(admin.ModelAdmin):
    inlines = [
        ProfileInline
    ]


admin.site.register(User, UserAdmin)
