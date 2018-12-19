class com.xeio.SwlMarket.Utils
{
    public static function Contains(array:Array, target):Boolean
    {
        for (var i:Number = 0 ; i < array.length ; i++)
        {
            if (array[i] == target)
            {
                return true;
            }
        }

        return false;
    }
    
    public static function Remove(array:Array, target):Void
    {
        for (var i:Number = 0 ; i < array.length ; i++)
        {
            if (array[i] == target)
            {
                array.splice(i, 1);
                return;
            }
        }
    }
    
    public static function Any(array:Array, func:Function):Boolean
	{
		for (var i:Number = 0 ; i < array.length ; i++)
		{
			if (func(array[i]))
			{
				return true;
			}
		}
		
		return false;
	}
    
    public static function FirstOrNull(array:Array, func:Function):Object
	{
		for (var i:Number = 0 ; i < array.length ; i++)
		{
			if (func(array[i]))
			{
				return array[i];
			}
		}
		
		return null;
	}
}