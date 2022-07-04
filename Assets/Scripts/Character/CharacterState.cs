public class CharacterState {
    public bool idle;
    public bool walking;
    public bool sliding;
    public bool jumping;
    public bool airborne;
    public bool chrouching;
    public bool hooking;

    public string GetState()
    {
        return $"hooking: {hooking}\njumping: {jumping}\nairborne: {airborne}\nwalking: {walking}\nsliding: {sliding}\nchrouching: {chrouching}\nidle: {idle}\n";
    }
};
