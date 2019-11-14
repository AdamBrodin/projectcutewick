/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */

public interface IKillable
{
    void ChangeHealth(int damage);
    void Die();

    int CurrentHealth { get; set; }
    int StartHealth { get; set; }
}
