from django.http import HttpResponse
from django.template import loader

def index(request):
    template = loader.get_template('game_temp.html')
    return HttpResponse(template.render())
