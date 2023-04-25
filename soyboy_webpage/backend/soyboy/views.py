from rest_framework import viewsets
from .models import Question, Comment
from .serializers import QuestionSerializer, CommentSerializer




class QuestionView(viewsets.ModelViewSet):
    serializer_class = QuestionSerializer
    queryset = Question.objects.all()

class CommentView(viewsets.ModelViewSet):
    serializer_class = CommentSerializer
    queryset = Comment.objects.all()

    