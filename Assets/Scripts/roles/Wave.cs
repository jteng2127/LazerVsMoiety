public class Wave : RoleManager
{
    // wave = -1 means that this the mower
    public int wave;

    public Wave()
    {
        roleType = new RoleType("Wave");
    }

    // Wave move right
    public void moveRight(int wave)
    {
        moveAToBbySpeed();
    }

    // Wave collision with functional group
    public void collision()
    {
        if(wave == -1)
        {
            // mower collision
            // do something 
            destory();
            return;
        }
        
        // wave collision
        // suppose fgID is Functional Group ID
        if(getWave(fgID) == wave)
        {
            destory();
        }
    }
}