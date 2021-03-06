﻿using System;
using Blake2Core;

namespace Chaos.NaCl.Internal.Ed25519Ref10
{
    internal static partial class Ed25519Operations
    {
        public static void crypto_sign_keypair(byte[] pk, int pkoffset, byte[] sk, int skoffset, byte[] seed, int seedoffset)
        {
            GroupElementP3 A;
            int i;

            Array.Copy(seed, seedoffset, sk, skoffset, 32);

            var blake2bConfig = new Blake2BConfig
            {
                OutputSizeInBytes = 64
            };
            var hasher = Blake2B.Create(blake2bConfig);
            hasher.Update(sk, skoffset, 32);
            byte[] h = hasher.Finish();
            //byte[] h = Sha512.Hash(sk, skoffset, 32);//ToDo: Remove alloc
            ScalarOperations.sc_clamp(h, 0);

            GroupOperations.ge_scalarmult_base(out A, h, 0);
            GroupOperations.ge_p3_tobytes(pk, pkoffset, ref A);

            for (i = 0; i < 32; ++i) sk[skoffset + 32 + i] = pk[pkoffset + i];
            CryptoBytes.Wipe(h);
        }

        public static void crypto_sign_keypair(byte[] pk, int pkoffset, byte[] sk, int skoffset)
        {
            GroupElementP3 A;
            int i;

            ScalarOperations.sc_clamp(sk, 0);

            GroupOperations.ge_scalarmult_base(out A, sk, 0);
            GroupOperations.ge_p3_tobytes(pk, pkoffset, ref A);

            for (i = 0; i < 32; ++i) sk[skoffset + 32 + i] = pk[pkoffset + i];
        }
    }
}
