from django.contrib import admin

from people.models import *


class ProfileInline(admin.TabularInline):
    model = Profile


admin.register(Profile)
