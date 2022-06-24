from django.contrib.auth import logout
from django.http import HttpResponseForbidden
from django.shortcuts import render, redirect
from django.views import View

from authentication.forms import *
from authentication.services import *

__all__ = ["LoginView", "SignUpView", "LogoutView"]


class LoginView(View):
    @staticmethod
    def get(request):
        if request.user.is_authenticated:
            return redirect("/")

        return render(request, "auth/auth.html",
                      {"form": LoginForm})

    @staticmethod
    def post(request):
        if request.user.is_authenticated:
            return HttpResponseForbidden()

        form = LoginForm(request.POST)
        if form.is_valid():
            if try_auth(form.cleaned_data["username"],
                        form.cleaned_data["password"], request):
                return redirect("/")
            print(request.user)
            form.add_error(None, "Incorrect login or password")

        return render(request, "auth/auth.html",
                      {"form": form})


class SignUpView(View):
    @staticmethod
    def get(request):
        if request.user.is_authenticated:
            return redirect("/")

        return render(request, "auth/auth.html",
                      {"form": SignUpForm})

    @staticmethod
    def post(request):
        if request.user.is_authenticated:
            return HttpResponseForbidden()

        form = SignUpForm(request.POST)
        if form.is_valid():
            create_new_user(form.cleaned_data["username"],
                            form.cleaned_data["password"], request)
            return redirect("/")

        return render(request, "auth/auth.html",
                      {"form": form})


class LogoutView(View):
    @staticmethod
    def get(request):
        logout(request)

        return redirect("/")
