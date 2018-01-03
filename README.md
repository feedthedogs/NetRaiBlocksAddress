# NetRaiBlocksAddress
Generate RaiBlocks Address' from a seed in .NET Core 2.0

Used PyRai as a reference: https://github.com/icarusglider/PyRai/

You can use it like this:

```c#
var seed = new Byte[] { 0x9F, 0x1D, 0x53, 0xE7, 0x32, 0xE4, 0x8F, 0x25, 0xF9, 0x47, 0x11, 0xD5, 0xB2, 0x20, 0x86, 0x77, 0x82, 0x78, 0x62, 0x4F, 0x71, 0x5D, 0x9B, 0x2B, 0xEC, 0x8F, 0xB8, 0x11, 0x34, 0xE7, 0xC9, 0x04 };

var wallet = new RaiWallet(seed);
for (int i = 0; i < 10; i++)
{
    var address = wallet.PublicAddress(i);
    Console.WriteLine("index: {0} address: {1} valid: {2}", i, address, wallet.ValidateAddress(address));
}
var badAddress = wallet.PublicAddress(1).Replace('1', '9');
Console.WriteLine("notvalid: {0} valid:{1}", badAddress, wallet.ValidateAddress(badAddress));
```

Output:
```
index: 0 address: xrb_34bmpi65zr967cdzy4uy4twu7mqs9nrm53r1penffmuex6ruqy8nxp7ms1h1 valid: True
index: 1 address: xrb_3pq7jpt1ixd6unh5pep4we1tmwnaydr1farsc81poeugkdsce7cain19myin valid: True
index: 2 address: xrb_1oajia6fspaim59wg15dmkondu6srhfcq45bwcnoe5khfj19tusqqaitnjfr valid: True
index: 3 address: xrb_1o5zaio4wukni5kbxed1jdcbxtz4s1eh4bcz7e19jtgq3wr4b3dgp7j89rga valid: True
index: 4 address: xrb_3jb5ob7w55dycppg9duagzcppij98nwdckst5hbfk8rmjcuenoxr8cqrkgir valid: True
index: 5 address: xrb_1mj46746jrom9p7xsx7uzn9o9urknqjufyrfrh4gch4icw69h4pheagmzr8q valid: True
index: 6 address: xrb_1o1ogsyty16axiutbus8wm66wmu563zt7d3qz51y7upho3hf4ejhxzqydp15 valid: True
index: 7 address: xrb_3ewducpjnfq87475ie1xrsst97rk4ttenax7tmwsjfqfjg85mei1szgwhypd valid: True
index: 8 address: xrb_3es787r9jeahahtmocog6y3o6ryc7b1nekxnz4pse1t5kzqoduna1gnh5cf6 valid: True
index: 9 address: xrb_335mzaj7gxwheu3o4jjgfw58y55mjnq44mcnsu6feda9xk8c491erdtoxyfz valid: True
notvalid: xrb_3pq7jpt9ixd6unh5pep4we9tmwnaydr9farsc89poeugkdsce7cain99myin valid:False
```