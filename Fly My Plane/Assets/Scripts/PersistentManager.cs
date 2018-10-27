using System.Collections;
using System.Collections.Generic;

public static class PersistentManager {

	private struct Enemy{
        public string name;
        public float health;
        public float damage;
        public float speed;
    }

    private struct Asteroid
    {
        public string name;
        public float health;
        public float damage;
        public float speed;
    }

    private static List<Enemy> listOfEnemies = new List<Enemy>();
    private static List<Asteroid> listOfAsteroids = new List<Asteroid>();

    public static void initEnemyList()
    {
        listOfEnemies.Add(new Enemy() { name = "Scout", health = 5, damage = 3, speed = 5 });
        listOfEnemies.Add(new Enemy() { name = "Fighter", health = 15, damage = 10, speed = 5 });
        listOfEnemies.Add(new Enemy() { name = "Bomber", health = 10, damage = 5, speed = 3 });
        listOfEnemies.Add(new Enemy() { name = "Merchant", health = 15, damage = 5, speed = 5 });
    }

    public static void initAsteroidList()
    {
        listOfAsteroids.Add(new Asteroid() { name = "Normal", health = 3, damage = 4, speed = 3 });
        listOfAsteroids.Add(new Asteroid() { name = "Small", health = 1, damage = 2, speed = 3 });
        listOfAsteroids.Add(new Asteroid() { name = "Armored", health = 6, damage = 4, speed = 3 });
        listOfAsteroids.Add(new Asteroid() { name = "Splitting", health = 3, damage = 4, speed = 3 });
        listOfAsteroids.Add(new Asteroid() { name = "Fast", health = 1, damage = 2, speed = 5 });
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
}
