namespace WibboEmulator.Games.Rooms.PathFinding
{
    public static class DreamPathfinder
    {
        public static SquarePoint GetNextStep(RoomUser user, int userX, int userY, int userTargetX, int userTargetY, byte[,] gameMap, double[,] height,
                byte[,] userOnMap, byte[,] squareTaking, int maxX, int maxY, bool userOverride, bool overrideHeight, bool diagonal, bool allowWalkthrough, bool oblique)
        {
            var map = new ModelInfo(maxX, maxY, gameMap, userOnMap, squareTaking);
            var target = new SquarePoint(user, userTargetX, userTargetY, userTargetX, userTargetY, map.GetState(userTargetX, userTargetY), userOverride, allowWalkthrough, map.GetStateUser(userTargetX, userTargetY));

            if (userX == userTargetX && userY == userTargetY)
            {
                return target;
            }

            return GetClosestSquare(new SquareInformation(user, userX, userY, target, map, userOverride, diagonal, allowWalkthrough, oblique), new HeightInfo(maxX, maxY, height), userOverride);
        }

        private static SquarePoint GetClosestSquare(SquareInformation info, HeightInfo height, bool userOverride)
        {
            var closest = info.Point.Distance;
            var squarePoint = info.Point;
            var state = height.GetState(info.Point.X, info.Point.Y);
            for (var val = 0; val < 8; val++)
            {
                var squarePoint2 = info.Pos(val);
                if (squarePoint2.AllowWalkthrough && squarePoint2.CanWalk(height.GetState(squarePoint2.X, squarePoint2.Y) - state) || userOverride)
                {
                    var getDistance = squarePoint2.Distance;
                    if (closest > getDistance)
                    {
                        closest = getDistance;
                        squarePoint = squarePoint2;
                    }
                }
            }

            return squarePoint;
        }
    }
}
