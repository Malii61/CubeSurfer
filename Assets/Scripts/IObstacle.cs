public interface IObstacle
{
    public void OnCollision(CollectableCube cube);
    public void AfterCollision(CollectableCube cube);
}
