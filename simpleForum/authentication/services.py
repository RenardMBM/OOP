from django.contrib.auth import authenticate, login, get_user_model

__all__ = ["try_auth", "create_new_user"]


def try_auth(username: str, password: str, request) -> bool:
    user = authenticate(username=username, password=password)

    if user is not None:
        login(request, user)

        return True
    return False


def create_new_user(username: str, password: str, request) -> None:
    user = get_user_model().objects.create_user(username=username, password=password)
    user.save()
    user = authenticate(username=username, password=password)
    login(request, user)
