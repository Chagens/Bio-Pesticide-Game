# Generated by Django 4.1.3 on 2022-11-11 17:10

from django.db import migrations, models


class Migration(migrations.Migration):

    dependencies = [
        ('game', '0002_player_money'),
    ]

    operations = [
        migrations.AlterField(
            model_name='player',
            name='money',
            field=models.IntegerField(blank=True, default=1000),
            preserve_default=False,
        ),
    ]
