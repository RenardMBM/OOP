from django.contrib.auth import get_user_model
from django.http import HttpResponseForbidden
from django.shortcuts import render, redirect, get_object_or_404
from django.views import View

from people.forms import SettingsForm

__all__ = ["IndexView", "ProfileView", "SettingsView"]


class IndexView(View):
    @staticmethod
    def get(request):
        return redirect("/")


class ProfileView(View):
    @staticmethod
    def get(request, uid):
        user = get_object_or_404(get_user_model(), id=uid)

        return render(request, "people/profile.html", {
            "user": request.user,
            "owner": user.profile,
            "is_owner": request.user.id == user.id})


class SettingsView(View):
    @staticmethod
    def get(request):
        if request.user.is_authenticated:
            return render(request, "people/settings.html",
                          {"user": request.user,
                           "form": SettingsForm(instance=request.user.profile)})

        return redirect("/")

    @staticmethod
    def post(request):
        if not request.user.is_authenticated:
            return HttpResponseForbidden()

        form = SettingsForm(request.POST, request.FILES, instance=request.user.profile)

        if form.is_valid():
            form.save()

            return redirect("/")

        return render(request, "people/settings.html",
                      {"user": request.user,
                       "form": form})
