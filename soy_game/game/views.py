from django.http import HttpResponse, HttpResponseRedirect
from django.template import loader
from django.urls import reverse
from .models import Players, Farms, Game

def index(request):
    players = Players.objects.all().values()
    template = loader.get_template('index.html')
    context = {
        'players': players,
    }
    return HttpResponse(template.render(context, request))

def add_player(request):
    player_name = request.POST['name']
    player = Players(name=player_name, money=1000, seeds=100, crops=0)
    player.save()

    farm = Farms(owner=player.id)
    farm.save()

    return HttpResponseRedirect(reverse('index'))

def delete_player(request, id):
    player = Players.objects.get(id=id)
    farms = Farms.objects.filter(owner=id).delete()
    player.delete()
    return HttpResponseRedirect(reverse('index'))

def start_game(request):
    game = Game()
    game.save()

    return HttpResponseRedirect(reverse('gamepage'))

def end_game(request):
    game = Game.objects.all()[0]
    for player in Players.objects.all():
        player.delete()

    for farm in Farms.objects.all():
        farm.delete()
    
    for game in Game.objects.all():
        game.delete()

    return HttpResponseRedirect(reverse('index'))


def gamepage(request):
    player_ids = Players.objects.order_by('id').values_list('id')
    game = Game.objects.all()[0]

    if game.player_turn >= len(player_ids):
        game.player_turn = 0
    
    player = Players.objects.get(id=player_ids[game.player_turn][0])
    farms = Farms.objects.filter(owner=player.id)
    
    template = loader.get_template('game_temp.html')
    context = {
        'player': player,
        'farms': farms,
        'game': game,
            }

    game.save()
    return HttpResponse(template.render(context, request))


def buy_land(request, id):
    game = Game.objects.all()[0]
    farm = Farms.objects.get(id=id)
    player = Players.objects.get(id=farm.owner)

    if player.money >= game.land_price:
        farm.size += 1
        player.money -= game.land_price

    farm.save()
    player.save()

    return HttpResponseRedirect(reverse('gamepage'))

def sell_land(request, id):
    game = Game.objects.all()[0]
    farm = Farms.objects.get(id=id)
    player = Players.objects.get(id=farm.owner)

    if farm.size > 0:
        farm.size -= 1
        player.money += game.land_price

    farm.save()
    player.save()

    return HttpResponseRedirect(reverse('gamepage'))

def plant(request, id):
    farm = Farms.objects.get(id=id)
    player = Players.objects.get(id=farm.owner)

    if player.seeds >= (farm.size * 100):
        farm.growing = True
        player.seeds -= (farm.size * 100)

    farm.save()
    player.save()

    return HttpResponseRedirect(reverse('gamepage'))

def harvest(request, id):
    farm = Farms.objects.get(id=id)
    player = Players.objects.get(id=farm.owner)

    player.crops += farm.harv_kill(player.growth_impact)

    farm.save()
    player.save()

    return HttpResponseRedirect(reverse('gamepage'))

def buy_farm(request, id):
    player = Players.objects.get(id=id)
    game = Game.objects.all()[0]

    if player.money >= game.farm_price:
        player.money -= game.farm_price
        farm = Farms(owner=player.id)

        player.save()
        farm.save()

    return HttpResponseRedirect(reverse('gamepage'))


def sell_farm(request, id):
    game = Game.object.all()[0]
    farm = Farms.objects.get(id=id)
    player = Players.objects.get(id=farm.owner)

    player.money += game.land_price * farm.size
    player.money += game.fram_price

    player.save()
    farm.delete()

    return HttpResponseRedirect(reverse('gamepage'))


def buy_seeds(request, id):
    game = Game.objects.all()[0]
    player = Players.objects.get(id=id)

    if player.money >= game.seed_price:
        player.money -= game.seed_price
        player.seeds += 100

    player.save()
    
    return HttpResponseRedirect(reverse('gamepage'))


def sell_crops(request, id):
    game = Game.objects.all()[0]
    player = Players.objects.get(id=id)

    player.money += (player.crops * game.crop_price * ((100+player.quality_impact)//100))
    player.crops = 0

    player.save()

    return HttpResponseRedirect(reverse('gamepage'))


def next_player(request):
    player_ids = Players.objects.order_by('id').values_list('id')
    game = Game.objects.all()[0]
    
    game.player_turn += 1

    if game.player_turn >= len(player_ids):
        game.player_turn = 0

        for farm in Farms.objects.all():
            owner = Players.objects.get(id=farm.owner)
            farm.update_crops(owner.growth_impact)
            farm.save()

    game.save()

    return HttpResponseRedirect(reverse('gamepage'))


