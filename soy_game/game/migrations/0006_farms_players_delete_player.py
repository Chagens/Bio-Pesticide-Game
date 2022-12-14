# Generated by Django 4.1.3 on 2022-11-13 19:06

from django.db import migrations, models


class Migration(migrations.Migration):

    dependencies = [
        ('game', '0005_alter_player_money'),
    ]

    operations = [
        migrations.CreateModel(
            name='Farms',
            fields=[
                ('id', models.BigAutoField(auto_created=True, primary_key=True, serialize=False, verbose_name='ID')),
                ('size', models.IntegerField(default=1)),
                ('owner', models.IntegerField(null=True)),
                ('crop_status', models.FloatField(default=0.0)),
                ('growing', models.BooleanField(default=False)),
            ],
        ),
        migrations.CreateModel(
            name='Players',
            fields=[
                ('id', models.BigAutoField(auto_created=True, primary_key=True, serialize=False, verbose_name='ID')),
                ('name', models.CharField(max_length=255)),
                ('money', models.IntegerField(default=1000)),
                ('seeds', models.IntegerField(default=100)),
                ('crops', models.IntegerField(default=0)),
                ('growth_impact', models.IntegerField(default=0)),
                ('quality_impact', models.IntegerField(default=0)),
            ],
        ),
        migrations.DeleteModel(
            name='Player',
        ),
    ]
