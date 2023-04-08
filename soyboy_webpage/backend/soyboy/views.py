from json import JSONDecodeError
from django.http import JsonResponse
from django.shortcuts import render
from rest_framework.parsers import JSONParser
from rest_framework.permissions import IsAuthenticated
from rest_framework import viewsets, status
from rest_framework.response import Response
from rest_framework.mixins import ListModelMixin, UpdateModelMixin, RetrieveModelMixin

from .serializers import QuestionSerializer
from .models import Question



class QuestionView(
    ListModelMixin,
    RetrieveModelMixin,
    viewsets.GenericViewSet
    ):

    permission_classes = (IsAuthenticated, )
    serializer_class = QuestionSerializer
    queryset = Question.objects.all()