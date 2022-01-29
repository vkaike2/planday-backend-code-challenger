using CarFactory_Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarFactory_Domain
{
    public abstract class PaintJob
    {
        public const string ALLOWED_CHARACTERS = "abcdefghijkmnopqrstuvwxyz0123456789";
        private bool IsUnlocked = false;
        private readonly string Solution;

        public Color BaseColor { get; protected set; }
        public Color StripeColor { get; protected set; }
        public Color DotColor { get; protected set; }

        public PaintJob()
        {
            Solution = CreateString(PuzzleAnswerLength());
        }
        public (int, long) CreateInstructions()
        {
            return (PuzzleAnswerLength(), EncodeString(Solution));
        }

        public bool TryUnlockInstructions(string answer)
        {
            if (AreInstructionsUnlocked()) throw new CarFactoryException("Paint Job is already unlocked");
            IsUnlocked = EncodeString(answer) == EncodeString(Solution);
            return IsUnlocked;
        }

        public bool AreInstructionsUnlocked() => IsUnlocked;

        protected abstract int PuzzleAnswerLength();

        public static long EncodeString(string text)
        {
            StringBuilder result = new StringBuilder();
            string key = "Planborghini";

            for (int charactere = 0; charactere < text.Length; charactere++)
            {
                result.Append((char)((uint)text[charactere] ^ (uint)key[charactere % key.Length]));
            }
            return result.ToString().GetHashCode();
        }

        private static string CreateString(int stringLength)
        {
            var rd = new Random(((int)DateTime.Now.Ticks) / 5 * 5);
            char[] chars = new char[stringLength];

            for (int i = 0; i < stringLength; i++)
            {
                chars[i] = ALLOWED_CHARACTERS[rd.Next(0, ALLOWED_CHARACTERS.Length)];
            }

            return new string(chars);
        }
    }

    public class SingleColorPaintJob : PaintJob
    {
        public SingleColorPaintJob(Color color) : base()
        {
            BaseColor = color;
        }

        protected override int PuzzleAnswerLength() => 2;
    }

    public class StripedPaintJob : PaintJob
    {
        public StripedPaintJob(Color baseCol, Color stripeCol) : base()
        {
            BaseColor = baseCol;
            StripeColor = stripeCol;
        }

        protected override int PuzzleAnswerLength() => 4;
    }

    public class DottedPaintJob : PaintJob
    {
        public DottedPaintJob(Color baseCol, Color dotCol) : base()
        {
            BaseColor = baseCol;
            DotColor = dotCol;
        }

        protected override int PuzzleAnswerLength() => 3;
    }
}
