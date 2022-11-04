using System.Xml.Linq;

namespace Lab_1
{
    public class MergeSort
    {
        private string PathA { get; set; }
        private string PathB { get; set; }
        private string PathC { get; set; }

        private string PathPrep = "prep.bin";

        private int parts = 1;
        private int bufferSizeForMerge = 524288;
        private int bufferSizeForSplit = Program.sizeInMB * 1;
        private int bufferSizeForPrep = Program.sizeInMB * 10;

        //сколько елементов надо взять до смены файла
        private long toTake = 1;

        public MergeSort(string pathA, string pathB, string pathC)
        {
            PathA = pathA;
            PathB = pathB;
            PathC = pathC;
        }

        public void Prep()
        {
            BinaryReader br = new BinaryReader(File.OpenRead(PathA));
            BinaryWriter writerPrep = new BinaryWriter(File.Create(PathPrep, 65536));

            byte[] mas;

            do
            {
                mas = br.ReadBytes(bufferSizeForPrep);
                Array.Sort(mas);
                writerPrep.Write(mas);
            }
            while (mas.Length == bufferSizeForPrep);

            br.Close();
            writerPrep.Close();
            toTake = bufferSizeForPrep;

            File.Delete(PathA);
            File.Move(PathPrep, PathA);
        }

        public void DefaultStart()
        {
            while (true)
            {
                Split();
                Merge();
                if (parts == 2)
                {
                    break;
                }
            }
        }


        public void OptimizedStart()
        {
            Prep();
            while (true)
            {
                SplitOptimized();
                MergeOptimized();
                if (parts == 2)
                {
                    break;
                }
            }
        }


        private void Split()
        {
            parts = 1;

            BinaryReader br = new BinaryReader(File.OpenRead(PathA));
            BinaryWriter writerB = new BinaryWriter(File.Create(PathB, 65536));
            BinaryWriter writerC = new BinaryWriter(File.Create(PathC, 65536));

            long counter = 0;
            bool flag = true; 

            long length = br.BaseStream.Length;
            long position = 0;
            byte element;
            while (position != length)
            {
                
                if (counter == toTake)
                {
                    flag = !flag;
                    counter = 0;
                    parts++;
                }

                element = br.ReadByte();
                position += 1;

                if (flag)
                {
                    writerB.Write(element);
                }
                else
                {
                    writerC.Write(element);
                }

                counter++;
            }

            br.Close();
            writerB.Close();
            writerC.Close();

        }


        private void SplitOptimized()
        {
            parts = 1;

            BinaryReader br = new BinaryReader(File.OpenRead(PathA));
            BinaryWriter writerB = new BinaryWriter(File.Create(PathB, 65536));  //65536
            BinaryWriter writerC = new BinaryWriter(File.Create(PathC, 65536));

            byte[] mas;

            long counter = 0;
            bool flag = true;

            do
            {
                mas = br.ReadBytes(bufferSizeForSplit);

                for (int i = 0; i < mas.Length; i++)
                {
                    if (counter == toTake)
                    {
                        flag = !flag;
                        counter = 0;
                        parts++;
                    }

                    if (flag)
                    {
                        writerB.Write(mas[i]);
                    }
                    else
                    {
                        writerC.Write(mas[i]);
                    }

                    counter++;
                }


            }
            while (mas.Length == bufferSizeForSplit);

            br.Close();
            writerB.Close();
            writerC.Close();
        }


        private void Merge()
        {
            BinaryReader readerB = new BinaryReader(File.OpenRead(PathB));
            BinaryReader readerC = new BinaryReader(File.OpenRead(PathC));
            BinaryWriter bw = new BinaryWriter(File.Create(PathA, 65536));


            long counterB = toTake, counterC = toTake;
            byte elementB = 0, elementC = 0;
            bool pickedB = false, pickedC = false, endB = false, endC = false;

            long lengthB = readerB.BaseStream.Length;
            long lengthC = readerC.BaseStream.Length;

            long positionB = 0;
            long positionC = 0;


            while (!endB || !endC)
            {
                if (counterB == 0 && counterC == 0)
                {
                    counterB = toTake;
                    counterC = toTake;
                }

                if (positionB != lengthB)
                {
                    if (counterB > 0 && !pickedB)
                    {
                        elementB = readerB.ReadByte();
                        positionB += 1;
                        pickedB = true;
                    }
                }
                else
                {
                    endB = true;
                }

                if (positionC != lengthC)
                {
                    if (counterC > 0 && !pickedC)
                    {
                        elementC = readerC.ReadByte();
                        positionC += 1;
                        pickedC = true;
                    }
                }
                else
                {
                    endC = true;
                }

                if (pickedB)
                {
                    if (pickedC)
                    {
                        if (elementB < elementC)
                        {
                            bw.Write(elementB);
                            counterB--;
                            pickedB = false;
                        }
                        else
                        {
                            bw.Write(elementC);
                            counterC--;
                            pickedC = false;
                        }
                    }
                    else
                    {
                        bw.Write(elementB);
                        counterB--;
                        pickedB = false;
                    }
                }
                else if (pickedC)
                {
                    bw.Write(elementC);
                    counterC--;
                    pickedC = false;
                }
            }

            toTake *= 2; 

            bw.Close();
            readerB.Close();
            readerC.Close();


        }


        private void MergeOptimized()
        {
            BinaryReader readerB = new BinaryReader(File.OpenRead(PathB));
            BinaryReader readerC = new BinaryReader(File.OpenRead(PathC));
            BinaryWriter bw = new BinaryWriter(File.Create(PathA, 65536));

            byte[] masB; byte[] masC;

            long counterB = toTake, counterC = toTake;
            byte elementB = 0, elementC = 0;
            bool pickedB = false, pickedC = false, endB = false, endC = false;

            long positionB;
            long positionC;
            long positionForWrite;

            do
            {
                masB = readerB.ReadBytes(bufferSizeForMerge);
                masC = readerC.ReadBytes(bufferSizeForMerge);
                byte[] masToWrite = new byte[masB.Length + masC.Length];
                positionForWrite = 0;
                positionB = 0;
                positionC = 0;
                pickedB = false;
                pickedC = false;
                endB = false;
                endC = false;


                while (!endB || !endC)
                {
                    if (counterB == 0 && counterC == 0)
                    {
                        counterB = toTake;
                        counterC = toTake;
                    }

                    if (positionB != masB.Length)
                    {
                        if (counterB > 0 && !pickedB)
                        {
                            elementB = masB[positionB];
                            positionB += 1;
                            pickedB = true;
                        }
                    }
                    else
                    {
                        endB = true;
                    }

                    if (positionC != masC.Length)
                    {
                        if (counterC > 0 && !pickedC)
                        {
                            elementC = masC[positionC];
                            positionC += 1;
                            pickedC = true;
                        }
                    }
                    else
                    {
                        endC = true;
                    }

                    if (pickedB)
                    {
                        if (pickedC)
                        {
                            if (elementB < elementC)
                            {
                                masToWrite[positionForWrite] = elementB;
                                positionForWrite++;
                                counterB--;
                                pickedB = false;
                            }
                            else
                            {
                                masToWrite[positionForWrite] = elementC;
                                positionForWrite++;
                                counterC--;
                                pickedC = false;
                            }
                        }
                        else
                        {
                            masToWrite[positionForWrite] = elementB;
                            positionForWrite++;
                            counterB--;
                            pickedB = false;
                        }
                    }
                    else if (pickedC)
                    {
                        masToWrite[positionForWrite] = elementC;
                        positionForWrite++;
                        counterC--;
                        pickedC = false;
                    }
                }

                bw.Write(masToWrite);


            }
            while (masB.Length == bufferSizeForMerge || masC.Length == bufferSizeForMerge);

            toTake *= 2; 

            bw.Close();
            readerB.Close();
            readerC.Close();
        }
    }
}
