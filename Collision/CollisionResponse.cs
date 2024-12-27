using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using TeamMilkGame.Commands;
using TeamMilkGame.XML;

namespace TeamMilkGame.Collision
{
    public class CollisionResponse
    {
        private static CollisionResponse instance = new CollisionResponse();
        // HashSet must be used for set key equality
        private IDictionary<HashSet<ICollidable.CollisionType>, IDictionary<ICollideSide.Side, IDictionary<ICollidable.CollisionType, Type>>> CollisionTable;
        private XMLCollisionHandler xmlCollisionHandler;
        public static CollisionResponse Instance
        {
            get
            {
                return instance;
            }
        }

        private CollisionResponse()
        {
            // Must use HashSet for set key equality
            CollisionTable = new Dictionary<HashSet<ICollidable.CollisionType>, IDictionary<ICollideSide.Side, IDictionary<ICollidable.CollisionType, Type>>>(HashSet<ICollidable.CollisionType>.CreateSetComparer());
            xmlCollisionHandler = new XMLCollisionHandler(CollisionTable);
            FillCollisionTable();
        }

        // Can be filled using XML
        private void FillCollisionTable()
        {
            xmlCollisionHandler.readXML();
        }

        private HashSet<ICollidable.CollisionType> CreateKey(ICollidable gameObj1, ICollidable gameObj2)
        {
            // Creates a HashSet key containing the two game objects
            return new HashSet<ICollidable.CollisionType>() { gameObj1.GetCollisionType(), gameObj2.GetCollisionType() };
        }

        public void HandleCollision(ICollidable gameObj1, ICollidable gameObj2, ICollideSide.Side Side, Rectangle Overlap)
        {
            // Key equality for sets in dictionaries only works with HashSet
            HashSet<ICollidable.CollisionType> key = CreateKey(gameObj1, gameObj2);

            // Find entry of two objects colliding. If not present, they don't collide and exit
            if (!CollisionTable.TryGetValue(key, out var Entry))
            {
                return;
            }

            //TODO: the keys in the collsion system arent based on "collider" and "collidee" but rather the group of the two objects together
            //we havent noticed it before because mario was always put into the lists first meaning hes checked first
            //so since hes put in the list at the end now, all moving objects can call methods on him from not his perspective
            //(e.g. gooba collides with him on bottom of mario so mario is damaged if mario jumped on top of him)
            //so for now im letting the system ignore it if mario is the "collidee" i.e. gameobject 2
            if (gameObj2.GetCollisionType() != ICollidable.CollisionType.Mario)
            {
                // Get Dictionary of Command types. If not found, default to null behavior
                if (!Entry.TryGetValue(Side, out IDictionary<ICollidable.CollisionType, Type> CmdTypes))
                {
                    CmdTypes = CollisionTable[key][ICollideSide.Side.Null];
                }

                // Search if gameObj1 needs a collision response
                if (CmdTypes.TryGetValue(gameObj1.GetCollisionType(), out Type CommandType))
                {
                    // Create the arguments for the constructor and execute the command
                    Type[] types = new Type[] { typeof(ICollidable), typeof(Rectangle) };
                    object[] objs = new object[] { gameObj1, Overlap };
                    ICommand Command = (ICommand)CommandType.GetConstructor(types).Invoke(objs);
                    Command.Execute();
                }

                if (CmdTypes.TryGetValue(gameObj2.GetCollisionType(), out CommandType))
                {
                    // Create the arguments for the constructor and execute the command
                    Type[] types = new Type[] { typeof(ICollidable), typeof(Rectangle) };
                    object[] objs = new object[] { gameObj2, Overlap };
                    ICommand Command = (ICommand)CommandType.GetConstructor(types).Invoke(objs);
                    Command.Execute();
                }
            }
        }
    }
}
