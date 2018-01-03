using System;
using System.Linq;
using Blake2Core;
using Chaos.NaCl;

namespace NetRaiBlocksAddress
{
    public class RaiWallet
    {
        readonly string _base32Alphabet = "13456789abcdefghijkmnopqrstuwxyz";
        byte[] _seed;

        public RaiWallet()
        {
            _seed = GenerateSeed();
        }

        public RaiWallet(byte[] seed)
        {
            _seed = seed;
        }

        public string PublicAddress(int index = 0)
        {
            var addressSeed = GenerateRaiAddressSeed(index);

            var pk = Ed25519.PublicKeyFromSeed(addressSeed);
            var pk32 = Base32withPadding.Encode(pk, _base32Alphabet, 260);

            var blake2bConfig = new Blake2BConfig
            {
                OutputSizeInBytes = 5
            };
            var hasher = Blake2B.Create(blake2bConfig);
            hasher.Update(pk);
            var checksum = hasher.Finish().Reverse().ToArray();
            var checksum32 = Base32withPadding.Encode(checksum, _base32Alphabet, 40);

            return $"xrb_{pk32}{checksum32}";
        }

        public byte[] SecretKey(int index = 0)
        {
            var addressSeed = GenerateRaiAddressSeed(index);
            return Ed25519.ExpandedPrivateKeyFromSeed(addressSeed);
        }

        public bool ValidateAddress(string address)
        {
            if (address.Length != 64 && !address.StartsWith("xrb_")) return false;
            var pk32 = address.Substring(4, 52);
            var pk = Base32withPadding.Decode(pk32, _base32Alphabet, 4);

            var checksum = address.Substring(56);
            var checksumDecoded = Base32withPadding.Decode(checksum, _base32Alphabet, 0);
            checksumDecoded = checksumDecoded.Reverse().ToArray();

            var blake2bConfig = new Blake2BConfig
            {
                OutputSizeInBytes = 5
            };
            var hasher = Blake2B.Create(blake2bConfig);
            hasher.Update(pk);
            var pkChecksum = hasher.Finish();
            return pkChecksum.SequenceEqual(checksumDecoded);
        }

        byte[] GenerateRaiAddressSeed(int index)
        {
            var blake2bConfig = new Blake2BConfig
            {
                OutputSizeInBytes = 32
            };
            var hasher = Blake2B.Create(blake2bConfig);
            hasher.Update(_seed);
            var indexBytes = BitConverter.GetBytes(index).Reverse().ToArray();
            hasher.Update(indexBytes);
            return hasher.Finish();
        }

        byte[] GenerateSeed()
        {
            byte[] array = new byte[32];
            Random random = new Random();
            random.NextBytes(array);
            return array;
        }
    }
}
