using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Time required for 3 friends to go to the destination: " + CalculateTimeFor3Friends(20, 5, 20) + "h");
            Console.WriteLine("Time required for 4 friends to go to the destination: " + ImprovedCalculateTimeForxFriends(20,5,20,4) + "h");

            Console.ReadKey();

        }

        /// <summary>
        /// a function that calculates the time that is needed for 3 friends to go to the destination 
        /// </summary>
        /// <param name="bikeSpeed">bike's speed in km/h</param>
        /// <param name="walkingSpeed">walking speed in km/h</param>
        /// <param name="destination">distance to the destination in km</param>
        /// <returns></returns>
        public static double CalculateTimeFor3Friends(int bikeSpeed, int walkingSpeed, int destination)
        {
            double bikeTime = Time(bikeSpeed, destination); //how much time it take the friend with the bike to go to the destination
            double walkingFriendDistance = Distance(walkingSpeed, bikeTime); // how much distance did the friend walk in that time
            double timeToGoToFriend = TimeToPickUpWalkingFriend(walkingSpeed, bikeSpeed, destination - walkingFriendDistance); //time to meet the walking friend for the bike friend

            return bikeTime + timeToGoToFriend;
        }

        /// <summary>
        /// a function that calculates the time that is needed for an x amount of friends to go to the destination 
        /// </summary>
        /// <param name="bikeSpeed">bike's speed in km/h</param>
        /// <param name="walkingSpeed">walking speed in km/h</param>
        /// <param name="destination">distance to the destination in km</param>
        /// <param name="friendCount">the number of friend going to the destination</param>
        /// <returns></returns>
        public static double ImprovedCalculateTimeForxFriends(int bikeSpeed, int walkingSpeed, int destination, int friendCount)
        {
            if (friendCount <= 2)
            {
                return Time(bikeSpeed, destination);
            }
            else
            {
                double allTime = 0;
                int leftFriends = friendCount;
                double leftDestination = destination;

                //calculations for the first bike trip for two friends
                leftFriends = leftFriends - 2;
                double bikeTime = Time(bikeSpeed, leftDestination);
                allTime += bikeTime;
                //picking up walking friends with bike
                while (leftFriends > 0)
                {
                    double walkingFriendDistance = Distance(walkingSpeed, bikeTime); // how much distance did the friend walk in the time
                    double timeToPickUpFriend = TimeToPickUpWalkingFriend(walkingSpeed, bikeSpeed, leftDestination - walkingFriendDistance); //time to meet the walking friend for the bike friend
                    leftDestination = leftDestination - walkingFriendDistance - Distance(walkingSpeed, timeToPickUpFriend);
                    leftFriends -= 1;
                    allTime += timeToPickUpFriend;
                    bikeTime = timeToPickUpFriend / 2; 
                }

                return allTime; 
            }

        }
        public static double Time(int speed, double distance)
        {
            return distance / speed; 
        }

        public static double Distance(int speed, double time)
        {
            return speed * time;
        }

        public static double TimeToPickUpWalkingFriend(int walkingSpeed, int bikeSpeed, double leftDistance)
        {
            return leftDistance / (walkingSpeed + bikeSpeed) * 2;
        }
    }
}
