from django import forms

from people.models import Profile

__all__ = ["SettingsForm"]


class SettingsForm(forms.ModelForm):
    class Meta:
        model = Profile
        exclude = ('user',)

