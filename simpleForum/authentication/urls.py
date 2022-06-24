from django.urls import path
from authentication.views import *

urlpatterns = [
    path("login/", LoginView.as_view()),
    path("new/", SignUpView.as_view()),
    path("logout/", LogoutView.as_view())
]
