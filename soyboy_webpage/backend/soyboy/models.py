from django.db import models
from django_extensions.db.models import TimeStampedModel

class Question(
    TimeStampedModel,
    models.Model
    ):
    
    title = models.CharField(max_length=255)
    information_url = models.CharField(max_length=255, blank=True, null=True)
    description = models.TextField(null=True)


    choice_a = models.CharField(max_length=255, blank=False)
    choice_b = models.CharField(max_length=255, blank=True, null=True)
    choice_c = models.CharField(max_length=255, blank=True, null=True)
    choice_d = models.CharField(max_length=255, blank=True, null=True)

    ANSWER_CHOICES = [
        ('choice_a', 'A'),
        ('choice_b', 'B'),
        ('choice_c', 'C'),
        ('choice_d', 'D'),
    ]

    answer = models.CharField(max_length=8, default='choice_a', choices=ANSWER_CHOICES)

    def __str__(self):
        return self.title



class Comment(
    TimeStampedModel,
    models.Model
    ):

    research_experience = models.CharField(max_length=255)
    username = models.CharField(max_length=255)
    subject = models.CharField(max_length=255)
    description = models.TextField()

    def __str__(self):
        return self.subject