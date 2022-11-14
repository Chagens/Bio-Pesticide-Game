from django.urls import path
from . import views

urlpatterns = [
        path('', views.index, name='index'),
        path('addplayer/', views.add_player, name='addplayer'),
        path('deleteplayer/<int:id>', views.delete_player, name='deleteplayer'),
        path('startgame/', views.start_game, name='startgame'),
        path('gamepage/endgame/', views.end_game, name='endgame'),
        path('gamepage/', views.gamepage, name='gamepage'),
        path('gamepage/buy_land/<int:id>', views.buy_land, name='buy_land'),
        path('gamepage/sell_land/<int:id>', views.sell_land, name='sell_land'),
        path('gamepage/plant/<int:id>', views.plant, name='plant'),
        path('gamepage/harvest/<int:id>', views.harvest, name='harvest'),
        path('gamepage/buyfarm/<int:id>', views.buy_farm, name='buy_land'),
        path('gamepage/nextplayer/', views.next_player, name='next_player'),
        path('gamepage/sellcrops/<int:id>', views.sell_crops, name='sell_crops'),
        path('gamepage/buyseeds/<int:id>', views.buy_seeds, name='buy_seeds'),
        path('gamepage/sellfarm/<int:id>', views.sell_farm, name='sell_farm'),
]
