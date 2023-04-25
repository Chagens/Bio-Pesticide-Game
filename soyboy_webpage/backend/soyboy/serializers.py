from rest_framework import serializers
from . import models

class QuestionSerializer(serializers.ModelSerializer):
    class Meta:
        model = models.Question
        fields = (
            'id',
            'title', 
            'description',
            'information_url',
            'choice_a', 
            'choice_b', 
            'choice_c', 
            'choice_d', 
            'answer',
        )

class CommentSerializer(serializers.ModelSerializer):
    class Meta:
        model = models.Comment
        fields = (
            'id',
            'research_experience',
            'username',
            'subject',
            'description'
        )