from django.urls import path, include
from django.contrib import admin
from rest_framework import routers
from soyboy import views as soyboy_views
from rest_framework.authtoken import views

router = routers.DefaultRouter()
router.register(r'questions', soyboy_views.QuestionView, 'question')
router.register(r'comments', soyboy_views.CommentView, 'comment')

urlpatterns = [
    path('admin/', admin.site.urls),
    path('api/', include(router.urls)),
    path('api-token-auth/', views.obtain_auth_token)
]