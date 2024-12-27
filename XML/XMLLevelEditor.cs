using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TeamMilkGame.UtilityClasses;

namespace TeamMilkGame.XML
{
    public class XMLLevelEditor
    {
        private XmlWriter writer;
        Dictionary<Type, Action> gameObjectMethods = new Dictionary<Type, Action>();
        private static XMLLevelEditor instance = new XMLLevelEditor();
        private IGameObject gameObject;

        public static XMLLevelEditor Instance { get { return instance; } }

        private XMLLevelEditor()
        {
            
            LoadDictionary();
            
        }

        private void LoadDictionary()
        {
            gameObjectMethods.Add(typeof(BrickBlock), writeBrickBlock);
            gameObjectMethods.Add(typeof(CrackedBrickBlock), writeCrackedBrickBlock);
            gameObjectMethods.Add(typeof(PipeBlock), writePipeBlock);
            gameObjectMethods.Add(typeof(QuestionBlock), writeQuestionBlock);
            gameObjectMethods.Add(typeof(SolidBlock), writeSolidBlock);
            gameObjectMethods.Add(typeof(Goomba), writeGoomba);
            gameObjectMethods.Add(typeof(Koopa), writeKoopa);
            gameObjectMethods.Add(typeof(Coin), writeCoin);
            gameObjectMethods.Add(typeof(ExtraLifeMushroom), writeExtraLifeMushroom);
            gameObjectMethods.Add(typeof(FireFlower), writeFireFlower);
            gameObjectMethods.Add(typeof(Flagpole), writeFlagpole);
            gameObjectMethods.Add(typeof(InvincibilityStar), writeStar);
            gameObjectMethods.Add(typeof(PowerMushroom), writePowerMushroom);
        }

        public void writeToFile(String fileName)
        {
            String xmlPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "./LevelXMLFiles/", fileName + ".xml");
            using (writer = new XmlTextWriter(File.Create(xmlPath), Encoding.UTF8))
            {
      
                writer.WriteStartDocument();

                //Write Level Name
                writer.WriteStartElement("Level");
                writer.WriteAttributeString("name", fileName);

                //Camera enabled for level
                writer.WriteElementString("Camera", "disabled");

                //Create background
                writeLevelData();

                //Create Ground
                writeGround();

                //Loop through list of game objects and write accordingly
                foreach (IGameObject gameObject in LevelEditorObjectManager.Instance.ObjectList)
                {
                    this.gameObject = gameObject;
                    gameObjectMethods[this.gameObject.GetType()]();

                }

                writer.WriteEndElement();

                writer.WriteEndDocument();
                writer.Close();
            }
        }

        private void writeLevelData()
        {
            writer.WriteStartElement("LevelData");
            writer.WriteElementString("BGName", "\"Super_Mario_1-1\"");
            writer.WriteElementString("Timer", "400");
            writer.WriteElementString("WorldNumber", "Custom");
            writer.WriteEndElement();
        }

        private void writeGround()
        {
            writer.WriteStartElement("Blocks");
            writer.WriteAttributeString("collidable", "1");
                writer.WriteStartElement("CrackedBrickBlock");
                    writer.WriteAttributeString("length", "69");
                    writer.WriteElementString("Position", "0 0");
                writer.WriteEndElement(); //Close CrackedBrickBlock element
            writer.WriteEndElement(); //Close Blocks element

            writer.WriteStartElement("Blocks");
            writer.WriteAttributeString("collidable", "0");

                writer.WriteStartElement("CrackedBrickBlock");
                writer.WriteAttributeString("length", "69");
                    writer.WriteElementString("Position", "0 -1");
                writer.WriteEndElement(); //Close CrackedBrickBlock element

                writer.WriteStartElement("CrackedBrickBlock");
                    writer.WriteAttributeString("length", "16");
                    writer.WriteElementString("Position", "71 -1");
                writer.WriteEndElement(); //Close CrackedBrickBlock element

                writer.WriteStartElement("CrackedBrickBlock");
                    writer.WriteAttributeString("length", "64");
                    writer.WriteElementString("Position", "90 -1");
                writer.WriteEndElement(); //Close CrackedBrickBlock element

                writer.WriteStartElement("CrackedBrickBlock");
                    writer.WriteAttributeString("length", "68");
                    writer.WriteElementString("Position", "156 -1");
                writer.WriteEndElement(); //Close CrackedBrickBlock element

            writer.WriteEndElement(); //Close Blocks element
        }

        private void writeBrickBlock()
        {
            writer.WriteStartElement("Blocks");
            writer.WriteAttributeString("collidable", "1");
            int posGridX = (int)gameObject.Position.X / GameUtility.Instance.GRID_SIZE;
            int posGridY = (int)gameObject.Position.Y / GameUtility.Instance.GRID_SIZE;
            writer.WriteStartElement("BrickBlock");
                    writer.WriteElementString("Position", posGridX + " " + posGridY);
                writer.WriteEndElement(); //Close BrickBlock Element

            writer.WriteEndElement(); //Close Block element
        }

        private void writeCrackedBrickBlock() 
        {
            writer.WriteStartElement("Blocks");
            writer.WriteAttributeString("collidable", "1");
            int posGridX = (int)gameObject.Position.X / GameUtility.Instance.GRID_SIZE;
            int posGridY = (int)gameObject.Position.Y / GameUtility.Instance.GRID_SIZE;
            writer.WriteStartElement("CrackedBrickBlock");
            writer.WriteElementString("Position", posGridX + " " + posGridY);
            writer.WriteEndElement(); //Close BrickBlock Element

            writer.WriteEndElement(); //Close Block element

        }

        private void writePipeBlock()
        {
            writer.WriteStartElement("Blocks");
            writer.WriteAttributeString("collidable", "1");
            int posGridX = (int)gameObject.Position.X / GameUtility.Instance.GRID_SIZE;
            int posGridY = (int)gameObject.Position.Y / GameUtility.Instance.GRID_SIZE;
            writer.WriteStartElement("PipeBlock");
            writer.WriteElementString("Position", posGridX + " " + posGridY);
            writer.WriteEndElement(); //Close BrickBlock Element

            writer.WriteEndElement(); //Close Block element
        }

        private void writeQuestionBlock()
        {
            writer.WriteStartElement("Blocks");
            writer.WriteAttributeString("collidable", "1");
            int posGridX = (int)gameObject.Position.X / GameUtility.Instance.GRID_SIZE;
            int posGridY = (int)gameObject.Position.Y / GameUtility.Instance.GRID_SIZE;
            writer.WriteStartElement("QuestionBlock");
            writer.WriteElementString("Position", posGridX + " " + posGridY);
            writer.WriteEndElement(); //Close BrickBlock Element

            writer.WriteEndElement(); //Close Block element
        }

        private void writeSolidBlock()
        {
            writer.WriteStartElement("Blocks");
            writer.WriteAttributeString("collidable", "1");
            int posGridX = (int)gameObject.Position.X / GameUtility.Instance.GRID_SIZE;
            int posGridY = (int)gameObject.Position.Y / GameUtility.Instance.GRID_SIZE;
            writer.WriteStartElement("SolidBlock");
            writer.WriteElementString("Position", posGridX + " " + posGridY);
            writer.WriteEndElement(); //Close BrickBlock Element

            writer.WriteEndElement(); //Close Block element
        }

        private void writeGoomba()
        {
            writer.WriteStartElement("Enemies");
            writer.WriteAttributeString("collidable", "1");
            int posGridX = (int)gameObject.Position.X / GameUtility.Instance.GRID_SIZE;
            int posGridY = (int)gameObject.Position.Y / GameUtility.Instance.GRID_SIZE;
            writer.WriteStartElement("Goomba");
            writer.WriteElementString("Position", posGridX + " " + posGridY);
            writer.WriteEndElement(); //Close Goomba Element

            writer.WriteEndElement(); //Close Enemies element
        }

        private void writeKoopa()
        {
            writer.WriteStartElement("Enemies");
            writer.WriteAttributeString("collidable", "1");
            int posGridX = (int)gameObject.Position.X / GameUtility.Instance.GRID_SIZE;
            int posGridY = (int)gameObject.Position.Y / GameUtility.Instance.GRID_SIZE;
            writer.WriteStartElement("Koopa");
            writer.WriteElementString("Position", posGridX + " " + posGridY);
            writer.WriteEndElement(); //Close Goomba Element

            writer.WriteEndElement(); //Close Enemies element
        }

        private void writeCoin()
        {
            writer.WriteStartElement("Items");
            writer.WriteAttributeString("collidable", "1");
            int posGridX = (int)gameObject.Position.X / GameUtility.Instance.GRID_SIZE;
            int posGridY = (int)gameObject.Position.Y / GameUtility.Instance.GRID_SIZE;
            writer.WriteStartElement("Coin");
            writer.WriteElementString("Position", posGridX + " " + posGridY);
            writer.WriteEndElement(); //Close Coin Element

            writer.WriteEndElement(); //Close Item element
        }

        private void writeExtraLifeMushroom()
        {
            writer.WriteStartElement("Items");
            writer.WriteAttributeString("collidable", "1");
            int posGridX = (int)gameObject.Position.X / GameUtility.Instance.GRID_SIZE;
            int posGridY = (int)gameObject.Position.Y / GameUtility.Instance.GRID_SIZE;
            writer.WriteStartElement("Coin");
            writer.WriteElementString("Position", posGridX + " " + posGridY);
            writer.WriteEndElement(); //Close Coin Element

            writer.WriteEndElement(); //Close Item element
        }

        private void writeFireFlower()
        {
            writer.WriteStartElement("Items");
            writer.WriteAttributeString("collidable", "1");
            int posGridX = (int)gameObject.Position.X / GameUtility.Instance.GRID_SIZE;
            int posGridY = (int)gameObject.Position.Y / GameUtility.Instance.GRID_SIZE;
            writer.WriteStartElement("FireFlower");
            writer.WriteElementString("Position", posGridX + " " + posGridY);
            writer.WriteEndElement(); //Close FireFlower Element

            writer.WriteEndElement(); //Close Item element
        }

        private void writeFlagpole()
        {
            writer.WriteStartElement("Items");
            writer.WriteAttributeString("collidable", "1");
            int posGridX = (int)gameObject.Position.X / GameUtility.Instance.GRID_SIZE;
            int posGridY = (int)gameObject.Position.Y / GameUtility.Instance.GRID_SIZE;
            writer.WriteStartElement("Flagpole");
            writer.WriteElementString("Position", posGridX + " " + posGridY);
            writer.WriteEndElement(); //Close Flagpole Element

            writer.WriteEndElement(); //Close Item element
        }

        private void writeStar()
        {
            writer.WriteStartElement("Items");
            writer.WriteAttributeString("collidable", "1");
            int posGridX = (int)gameObject.Position.X / GameUtility.Instance.GRID_SIZE;
            int posGridY = (int)gameObject.Position.Y / GameUtility.Instance.GRID_SIZE;
            writer.WriteStartElement("InvincibilityStar");
            writer.WriteElementString("Position", posGridX + " " + posGridY);
            writer.WriteEndElement(); //Close InvicibilityStar Element

            writer.WriteEndElement(); //Close Item element
        }

        private void writePowerMushroom()
        {
            writer.WriteStartElement("Items");
            writer.WriteAttributeString("collidable", "1");
            int posGridX = (int)gameObject.Position.X / GameUtility.Instance.GRID_SIZE;
            int posGridY = (int)gameObject.Position.Y / GameUtility.Instance.GRID_SIZE;
            writer.WriteStartElement("PowerMushroom");
            writer.WriteElementString("Position", posGridX + " " + posGridY);
            writer.WriteEndElement(); //Close PowerMushroom Element

            writer.WriteEndElement(); //Close Item element
        }
    }

}
