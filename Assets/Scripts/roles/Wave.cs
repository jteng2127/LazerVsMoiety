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
        _move();
    }

    public void collision()
    {
        if(wave == -1)
        {
            // mower collision
            // do something 
            _destory();
            return;
        }
        
        // wave collision
        // suppose fgID is Functional Group ID
        if(getWave(fgID) == wave)
        {
            _destory();
        }
    }
}