from json import JSONDecodeError
from django.http import JsonResponse
from django.shortcuts import render
from rest_framework.parsers import JSONParser
from rest_framework.permissions import IsAuthenticated
from rest_framework import viewsets, status
from rest_framework.response import Response
from rest_framework.mixins import ListModelMixin, UpdateModelMixin, RetrieveModelMixin
from rest_framework.authentication import TokenAuthentication
from rest_framework.permissions import AllowAny
from django.contrib.auth.models import User



from .serializers import QuestionSerializer, CommentSerializer
from .models import Question, Comment



class QuestionView(
    ListModelMixin,
    RetrieveModelMixin,
    viewsets.GenericViewSet
    ):

    permission_classes = (IsAuthenticated, )
    serializer_class = QuestionSerializer
    queryset = Question.objects.all()

class CommentView(
    viewsets.ModelViewSet
):
    serializer_class = CommentSerializer
    queryset = Comment.objects.all()

    