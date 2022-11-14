from django.db import models

class Farms(models.Model):
    size = models.IntegerField(default=1)
    owner = models.IntegerField(null=True)
    crop_status = models.FloatField(default=0.0)
    growing = models.BooleanField(default=False)

    def update_crops(self, growth_impact=0):
        if self.growing:
            self.crop_status += (10 * (100 / (100 + growth_impact)))
            if self.crop_status >= 100:
                self.crop_status = 100
                self.growing = False

    def harv_kill(self, growth_impact):
        if self.crop_status >= 100:
            self.crop_status = 0.0
            self.growing = False
            return (self.size * (100+growth_impact))
        else:
            self.crop_status = 0.0
            self.growing = False
            return 0

class Players(models.Model):
    name = models.CharField(max_length=255)
    money = models.IntegerField(default=1000)
    seeds = models.IntegerField(default=100)
    crops = models.IntegerField(default=0)
    
    growth_impact = models.IntegerField(default=0)
    quality_impact = models.IntegerField(default=0)


class Game(models.Model):
    crop_price = models.IntegerField(default=1)
    seed_price = models.IntegerField(default=25)
    farm_price = models.IntegerField(default=1000)
    land_price = models.IntegerField(default=500)

    season = models.CharField(max_length=255, default='Spring')
    player_turn = models.IntegerField(default=0)



