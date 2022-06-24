from django.db import models
from authentication.models import User
from django.db.models.signals import post_save
from django.dispatch import receiver
from django.core.exceptions import ObjectDoesNotExist

__all__ = ["Profile"]


class Profile(models.Model):
    user = models.OneToOneField(User, on_delete=models.CASCADE)
    name = models.CharField(max_length=64, default="Stranger")
    about = models.TextField(default="I'm using SimpleForum")
    image = models.ImageField(upload_to="img/", default="img/default.jpg")


@receiver(post_save, sender=User)
def save_or_create_profile(sender, instance, created, **kwargs):
    if created:
        Profile.objects.create(user=instance)

    else:
        try:
            instance.profile.save()

        except ObjectDoesNotExist:
            Profile.objects.create(user=instance)
