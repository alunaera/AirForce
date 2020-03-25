namespace AirForce
{
    internal abstract class ArmedGameObject : GameObject
    {
        protected int DelayOfShot;

        protected void DecreaseDelayOfShot(int valueOfDecrease)
        {
            if (DelayOfShot > 0)
                DelayOfShot -= valueOfDecrease;
        }

        protected void ReloadWeapon()
        {
            DelayOfShot = 150;
        }
    }
}
