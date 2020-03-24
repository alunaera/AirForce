namespace AirForce
{
    internal abstract class ArmedGameObject : GameObject
    {
        protected int DelayOfShot;

        protected void DecreaseDelayOfShot(int valueOfIncrease)
        {
            if (DelayOfShot > 0)
                DelayOfShot -= valueOfIncrease;
        }

        protected void ReloadWeapon()
        {
            DelayOfShot = 150;
        }
    }
}
