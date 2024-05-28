using System;
using System.Collections.Generic;
using System.Drawing;
using Master_Jump.Models;
using Master_Jump.Models.Interfaces;

namespace Master_Jump.Controllers
{
    public static class PlatformController
    {
        public static readonly List<IPlatform> Platforms = new List<IPlatform>();

        public static int _startYPosition = 400;

        private static void AddPlatform(IPlatform platform)
        {
            CollisionCheck(platform);
            Platforms.Add(platform);
        }
        
        public static void GenerateStartPlatforms()
        {
            Random random = new Random();
            for (int i = 0;i<10;i++)
            {
                int x = random.Next(0, 270);
                int y = random.Next(35, 50);
                _startYPosition -= y;
                Point position = new Point(x, _startYPosition);
                AddPlatform(new BasePlatform(position));
            }
        }

        public static void GeneratePlatforms()
        {
            Random random = new Random();
            int x = random.Next(0, 255);
            int y = 90;
            Point position = new Point(x, _startYPosition - y);
            AddPlatform(new BasePlatform(position));
            
            int enemyChance = random.Next(0, 100);
            if (enemyChance > 80)
            {
                position.X += 10;
                position.Y -= 38;
                EnemyController.GenerateEnemy(new Model(position, new SizeF(40,40)));
            }
            
            int brokenChance = random.Next(0, 100);
            if (brokenChance>30) return;
            GenerateBrokenPlatform(x);
        }

        private static void GenerateBrokenPlatform(int occupiedCoordinates)
        {
            int platformLength = 108;
            int finish = platformLength + occupiedCoordinates;
            int start = 0;
            if (occupiedCoordinates - platformLength > 0) start = occupiedCoordinates - platformLength;
            if (330 - finish < platformLength)
            {
                finish = 330;
                platformLength = 0;
            }
            
            int[] availableCoordinates = new int[start + (330-finish-platformLength)];

            for (int i = 0;i<start;i++)
            {
                availableCoordinates[i] = i + 1;
            }

            for (int i = start;i<availableCoordinates.Length;i++, finish++)
            {
                availableCoordinates[i] = finish;
            }

            Random random = new Random();

            int[] yOffsetArray = new int[40];
            for (int i = 40, k =-40, m =0, z = yOffsetArray.Length-1;i != 20 && k!= -20;k++,i--, m++, z--)
            {
                yOffsetArray[m] = k;
                yOffsetArray[z] = i;
            }
            
            Point coordinates = new Point(availableCoordinates[random.Next(0, availableCoordinates.Length-1)], _startYPosition-90 - yOffsetArray[random.Next(0, yOffsetArray.Length)] );
            IPlatform crushedPlatform = new BrokenPlatform(coordinates);
            AddPlatform(crushedPlatform);
        }
        
        public static void ClearPlatforms()
        {
            Platforms.RemoveAll(p => p.Model.Coordinates.Y >= 550 || p.Model.Coordinates.Y < -150);
        }

        private static void CollisionCheck(IPlatform platform)
        {
            foreach (var plat in PlatformController.Platforms)
            {
                if (platform.Model.Coordinates.X < plat.Model.Coordinates.X + plat.Model.Size.Width &&
                    platform.Model.Coordinates.X + platform.Model.Size.Width > plat.Model.Coordinates.X &&
                    platform.Model.Coordinates.Y < plat.Model.Coordinates.Y + plat.Model.Size.Height &&
                    platform.Model.Coordinates.Y + platform.Model.Size.Height > plat.Model.Coordinates.Y)
                {
                    platform.Model.Coordinates.Y -= 15;
                    CollisionCheck(platform);
                }
            }
        }
    }
}