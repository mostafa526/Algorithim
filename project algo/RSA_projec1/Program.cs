using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RSA_Project
{
    class Program
    {

        static void Main(string[] args)
        {
           // Console.SetIn(new StreamReader(Console.OpenStandardInput(8192)));
           // Console.Write("Enter First Number : ");
           // string ms = Console.ReadLine();
           // Big_int bi = new Big_int(ms);
           // Console.Write("Enter Second Number : ");
           // string ms2 = Console.ReadLine();
           // Big_int bi2 = new Big_int(ms2);
           // Big_int res = new Big_int(ms.Length);
           // //Big_int.ADD(bi, bi2, ref res);
           // Big_int.sub(bi, bi2, ref res);
           // //Big_int.mult(bi, bi2,ref res);
           //// Big_int.add_zeros(ref bi,ref bi2);
           // foreach (int i in res.arr)
           // {
           //     Console.Write(i);
           // }
           // Console.Write("\n");

            Console.SetIn(new StreamReader(Console.OpenStandardInput(8192)));
            //Console.Write("Enter Number : ");
            //string ms = Console.ReadLine();
            Big_int bi = new Big_int("35052111338673026690212423937053328511880760811579981620642802346685810623109850235943049080973386241113784040794704193978215378499765413083646438784740952306932534945195080183861574225226218879827232453912820596886440377536082465681750074417459151485407445862511023472235560823053497791518928820272257787786");
            //Console.Write("Enter Power : ");
            //string ms2 = Console.ReadLine();
            Big_int bi2 = new Big_int("89489425009274444368228545921773093919669586065884257445497854456487674839629818390934941973262879616797970608917283679875499331574161113854088813275488110588247193077582527278437906504015680623423550067240042466665654232383502922215493623289472138866445818789127946123407807725702626644091036502372545139713");
            //Console.Write("Enter Mod : ");
            //string ms3 = Console.ReadLine();
            Big_int bi3 = new Big_int("145906768007583323230186939349070635292401872375357164399581871019873438799005358938369571402670149802121818086292467422828157022922076746906543401224889672472407926969987100581290103199317858753663710862357656510507883714297115637342788911463535102712032765166518411726859837988672111837205085526346618740053");

            Big_int res = new Big_int(bi.arr.Length);
            Big_int q = new Big_int(bi.arr.Length);
            Big_int r = new Big_int(bi.arr.Length);
            Big_int.div(bi, bi3, ref q, ref r);

            Big_int.pow_mod(r, bi2, bi3, ref res);
            res = Big_int.remove_zeros(res);
            foreach (int i in res.arr)
            {
                Console.Write(i);
            }
            Console.WriteLine();
        }
    }
}
