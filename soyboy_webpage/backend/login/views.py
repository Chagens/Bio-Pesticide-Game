from rest_framework.permissions import AllowAny
from rest_framework.views import APIView
from rest_framework.response import Response
from .serializers import UserSerializer, RegisterSerializer
from django.contrib.auth.models import User
from rest_framework.authentication import TokenAuthentication
from rest_framework import generics
from rest_framework import viewsets
from rest_framework.mixins import ListModelMixin, RetrieveModelMixin




# Class based view to Get User Details using Token Authentication
class UserDetailAPI(
    ListModelMixin,
    RetrieveModelMixin,
    viewsets.GenericViewSet
    ):

    authentication_classes = (TokenAuthentication, )
    permission_classes = (AllowAny, )

    def get(self, request, *args, **kwargs):
        user = User.objects.get(id=request.user.id)
        serializer = UserSerializer(user)
        return Response(serializer.data)
    

# Class baesd view to register user
class RegisterUserAPIView(
    ListModelMixin,
    RetrieveModelMixin,
    viewsets.GenericViewSet
    ):
    
    permission_classes = (AllowAny, )
    serializer_class = RegisterSerializer