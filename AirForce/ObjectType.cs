using System;

namespace AirForce
{
    [Flags]
    internal enum ObjectType
    {
        PlayerShip = 1,
        PlayerBullet = 2,
        ChaserShip = 4,
        BomberShip = 8,
        BomberShipBullet = 16,
        Meteor = 32,
        Ground = 64,
        Bird = 128,
        Blast = 256
    }
}
