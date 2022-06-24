from django.db import models
from authentication.models import User

__all__ = ["Profile"]


class Profile(models.Model):
    user = models.OneToOneField(User, on_delete=models.CASCADE)
    name = models.CharField(max_length=64, default="Stranger")
    about = models.TextField(default="I'm using SimpleForum")
    image = models.ImageField(upload_to="img/", default="img/default.jpg")

