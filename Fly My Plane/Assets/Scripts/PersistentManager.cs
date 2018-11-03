using System.Collections;
using System.Collections.Generic;

public static class PersistentManager {

	private struct Enemy{
        public string name;
        public float health;
        public float bulletDamage;
        public float damage;
        public float speed;
        public int score;
    }

    private struct Asteroid
    {
        public string name;
        public float health;
        public float damage;
        public float speed;
        public int score;
    }

    private static List<Enemy> listOfEnemies = new List<Enemy>();
    private static List<Asteroid> listOfAsteroids = new List<Asteroid>();
    private static int[] offensiveUpgrade = { 5, 6, 7, 8, 9, 10 };
    private static int[] defensiveUpgrade = { 10, 12, 15, 19, 24, 30 };

    public static void initEnemyList()
    {
        listOfEnemies.Add(new Enemy() { name = "Scout", health = 5, bulletDamage = 0, damage = 3, speed = 5, score = 20 });
        listOfEnemies.Add(new Enemy() { name = "Fighter", health = 5, bulletDamage = 5, damage = 4, speed = 5, score = 20 });
        listOfEnemies.Add(new Enemy() { name = "Bomber", health = 10, bulletDamage = 5, damage = 5, speed = 3, score = 20 });
        listOfEnemies.Add(new Enemy() { name = "Merchant", health = 10, bulletDamage = 0, damage = 5, speed = 5, score = 20 });
    }

    public static void initAsteroidList()
    {
        listOfAsteroids.Add(new Asteroid() { name = "Normal", health = 10, damage = 4, speed = 3, score = 10 });
        listOfAsteroids.Add(new Asteroid() { name = "Small", health = 10, damage = 2, speed = 3, score = 5 });
        listOfAsteroids.Add(new Asteroid() { name = "Armored", health = 20, damage = 4, speed = 3, score = 15 });
        listOfAsteroids.Add(new Asteroid() { name = "Splitting", health = 10, damage = 4, speed = 3, score = 5 });
        listOfAsteroids.Add(new Asteroid() { name = "Fast", health = 10, damage = 2, speed = 5, score = 5 });
    }

    //Enemy Getter
    public static string getEnemyName(int _enemyId)
    {
        return listOfEnemies[_enemyId - 1].name;
    }
    public static float getEnemyHealth(int _enemyId)
    {
        return listOfEnemies[_enemyId - 1].health;
    }
    public static float getEnemyDamage(int _enemyId)
    {
        return listOfEnemies[_enemyId - 1].damage;
    }
    public static float getEnemySpeed(int _enemyId)
    {
        return listOfEnemies[_enemyId - 1].speed;
    }
    public static int getEnemyScore(int _enemyId)
    {
        return listOfEnemies[_enemyId - 1].score;
    }

    //Asteroid Getter
    public static string getAsteroidName(int _asteroidId)
    {
        return listOfAsteroids[_asteroidId - 1].name;
    }
    public static float getAsteroidHealth(int _asteroidId)
    {
        return listOfAsteroids[_asteroidId - 1].health;
    }
    public static float getAsteroidDamage(int _asteroidId)
    {
        return listOfAsteroids[_asteroidId - 1].damage;
    }
    public static float getAsteroidSpeed(int _asteroidId)
    {
        return listOfAsteroids[_asteroidId - 1].speed;
    }
    public static int getAsteroidScore(int _asteroidId)
    {
        return listOfAsteroids[_asteroidId - 1].score;
    }

    //Offensive Upgrade Getter
    public static int getOffensiveLevelUpgrade(int level)
    {
        return offensiveUpgrade[level];
    }

    //Defensive Upgrade Getter
    public static int getDefensiveLevelUpgrade(int level)
    {
        return defensiveUpgrade[level];
    }
}
