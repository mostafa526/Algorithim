using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace RSA_Project
{
    public class Big_int
    {

        public int[] arr; //O(1)
        public Big_int(string msg) //O(msg.Length) -> O(N)
        {
            this.arr = new int[msg.Length]; //O(1)
            for (int i = 0; i < msg.Length; i++) //O(msg.Length) -> O(N)
            {
                //arr[i] = Convert.ToInt32(msg[i]);//ll7roof
                this.arr[i] = (int)Char.GetNumericValue(msg[i]); //O(1)
            }
        }
        public Big_int(int length) //O(1)
        {
            this.arr = new int[length]; //O(1)
        }
        public static Big_int ADD(Big_int str1, Big_int str2, ref Big_int res)
        {

            int numofzero; //O(1)
            int carry = 0; //O(1)
            Big_int total;  //O(1)
            if (str1.arr.Length > str2.arr.Length) //O(1)
            {
                numofzero = str1.arr.Length - str2.arr.Length; //O(1)
                total = new Big_int(str1.arr.Length + 1); //O(1)

                for (int i = str2.arr.Length - 1; i >= 0; i--) //O(Str2.length ) -> O(N)
                {
                    int num = 0; //O(1)

                    num = str1.arr[i + numofzero] + str2.arr[i] + carry; //O(1)
                    if (num >= 10) //O(1)
                    {
                        total.arr[i + numofzero + 1] = num % 10; //O(1)
                        carry = 1; //O(1)
                    }
                    else
                    {
                        total.arr[i + numofzero + 1] = num; //O(1)
                        carry = 0;//O(1)
                    }
                }
                for (int i = numofzero; i > 0; i--)//O(numofzero)
                {
                    total.arr[i] = str1.arr[i - 1] + carry; //O(1)
                    if (total.arr[i] >= 10) //O(1)
                    {
                        total.arr[i] = total.arr[i] % 10; //O(1)
                        carry = 1; //O(1)
                    }
                    else
                    {
                        carry = 0; //O(1)
                    }
                }
                total.arr[0] = carry; //O(1)
            }

            else
            {
                numofzero = str2.arr.Length - str1.arr.Length; //O(1)
                total = new Big_int(str2.arr.Length + 1); //O(1)

                for (int i = str1.arr.Length - 1; i >= 0; i--)//O(Str1.length ) -> O(N)
                {
                    int num = 0; //O(1)
                    num = str1.arr[i] + str2.arr[i + numofzero] + carry; //O(1)

                    if (num >= 10) //O(1)
                    {
                        total.arr[i + numofzero + 1] = num % 10; //O(1)
                        carry = 1;//O(1)
                    }
                    else
                    {
                        total.arr[i + numofzero + 1] = num; //O(1)
                        carry = 0; //O(1)
                    }

                }
                for (int i = numofzero; i > 0; i--)//O(numofzero)
                {
                    total.arr[i] = str2.arr[i - 1] + carry; //O(1)
                    if (total.arr[i] >= 10) //O(1)
                    {
                        total.arr[i] = total.arr[i] % 10; //O(1)
                        carry = 1; //O(1)
                    }
                    else
                    {
                        carry = 0; //O(1)
                    }
                }
                total.arr[0] = carry; //O(1)
            }
            int non_zero = 0; //O(1)
            for (int i = 0; i < total.arr.Length; i++) //O(total.arr.length)
            {
                if (total.arr[i] == 0)
                {
                    continue; //O(1)
                }
                non_zero = i; //O(1)
                break; //O(1)
            }
            res = new Big_int(total.arr.Length - non_zero); //O(1)
            for (int i = 0; i < res.arr.Length; i++) //O(res.arr.length)
            {
                res.arr[i] = total.arr[i + non_zero]; //O(1)
            }
            return res; //O(1)
        }
        // complixty of fun add ( O(res.arr.length) + O(total.arr.length) + O(Str1.length ) +O(Str2.length ) + O(1) + O(1)
        // in exact case all = N 
        // total complixty = 2N = O(N)

        public static Big_int sub(Big_int fir_num, Big_int sec_num, ref Big_int res)
        {
            Big_int comp = new Big_int(fir_num.arr.Length);           //O(1)
            int diff = fir_num.arr.Length - sec_num.arr.Length;      //O(1)
            for (int i = diff; i < fir_num.arr.Length; i++)         //O(N)  
            {
                comp.arr[i] = 9-sec_num.arr[i-diff];               // O(1)
            }   
            for (int i = 0; i < diff; i++)                        //  O(diff)...O(N)
            {
                comp.arr[i] = 9;                                  // O(1)
            }
            Big_int one = new Big_int(1);                         // O(1)
            one.arr[0]=1;                                         // O(1)
            ADD(comp, one, ref comp);                             // O(N)
            comp = remove_zeros(comp);                            // O(N)
            ADD(fir_num, comp, ref comp);                         // O(N)
            comp = remove_zeros(comp);                            // O(N)
            res = new Big_int(comp.arr.Length-1);                 // O(1)  
            for (int i = 1; i < comp.arr.Length; i++)              // O(N)
            {
                res.arr[i - 1] = comp.arr[i];                      //O(1)  
            }
            res = remove_zeros(res);                              //O(N)
            return res;                                           // O(1)
        }
        // complixty of fun add ( O(fir_num.arr.Length) + O(diff) + O(comp.arr.Length) + O(N) + O(N) + O(N) + O(N) + O(N)
        // in exact case all = N 
        // total complixty = 8N = O(N) 
        public static Big_int mult(Big_int x, Big_int y, ref Big_int res)
        {

            int max = Math.Max(x.arr.Length, y.arr.Length); //O(1)
            int mid = max / 2;//O(1)
            if (x.arr.Length == 1 && y.arr.Length == 1)
            {
                res.arr[res.arr.Length - 1] = x.arr[mid] * y.arr[mid];//O(1)
                int num = res.arr[res.arr.Length - 1] % 10; //O(1)
                int num2 = res.arr[res.arr.Length - 1] / 10;//O(1)
                if (res.arr[res.arr.Length - 1] / 10 != 0)
                {
                    res = new Big_int(2); //O(1)
                    res.arr[res.arr.Length - 1] = num; //O(1)
                    res.arr[res.arr.Length - 2] = num2; //O(1)
                }
                return res;//O(1)
            }
            if ((max == x.arr.Length && max % 2 != 0) || (max == y.arr.Length && max % 2 != 0))
            {
                max += 1; //O(1)
                string x_str = String.Join("", x.arr);  //O(N)
                string y_str = String.Join("", y.arr);//O(N)
                x_str = x_str.PadLeft(max, '0');//O(N)
                y_str = y_str.PadLeft(max, '0');//O(N)

                x = new Big_int(x_str); //O(x_str)
                y = new Big_int(y_str);//O(y_str)
            }
            else if (x.arr.Length != y.arr.Length)
            {
                if (x.arr.Length < y.arr.Length)
                {
                    string x_str = String.Join("", x.arr); //O(N)
                    x_str = x_str.PadLeft(max, '0'); //O(N)
                    x = new Big_int(x_str); //O(x_str)
                }
                else
                {
                    string y_str = String.Join("", y.arr);//O(N)
                    y_str = y_str.PadLeft(max, '0');//O(N)
                    y = new Big_int(y_str);//O(y_str)
                }
            }
            mid = max / 2;//O(1)

            Big_int a = new Big_int(mid);   //O(1)
            Big_int b = new Big_int(mid); //O(1)
            Big_int c = new Big_int(mid); //O(1)
            Big_int d = new Big_int(mid); //O(1)
            for (int i = 0; i < mid; i++)//O(mid)
            {
                b.arr[i] = x.arr[i]; //O(1)
                d.arr[i] = y.arr[i];  //O(1)
            }
            for (int i = mid; i < max; i++) //O(mid)
            {
                a.arr[i - mid] = x.arr[i]; //O(1)
                c.arr[i - mid] = y.arr[i]; //O(1)
            }
            Big_int ac = new Big_int(mid); //O(1)

            mult(a, c, ref ac); //T(N/2)
            ac = remove_zeros(ac);//O(N)
            Big_int bd = new Big_int(mid); //O(1)
            mult(b, d, ref bd); //T(N/2)
            bd = remove_zeros(bd); //O(N)
            Big_int acPbd = new Big_int(0); //O(1)
            ADD(ac, bd, ref acPbd); //O(N)
            acPbd = remove_zeros(acPbd); //O(N)
            Big_int aPb = new Big_int(0);  //O(1)
            Big_int.ADD(a, b, ref aPb); //O(N)
            aPb = remove_zeros(aPb); //O(N)
            Big_int cPd = new Big_int(0); //O(1)
            Big_int.ADD(c, d, ref cPd);//O(N)
            cPd = remove_zeros(cPd); //O(N)

            Big_int aPbcPd = new Big_int(mid); //O(1)
            mult(aPb, cPd, ref aPbcPd); //T(N/2)
            aPbcPd = remove_zeros(aPbcPd); //O(N)
            Big_int subt = new Big_int(0); //O(1)
            sub(aPbcPd, acPbd, ref subt); //O(N)
            Big_int bdz = new Big_int(x.arr.Length + bd.arr.Length); //O(1)
            for (int i = 0; i < bd.arr.Length; i++) //O(bd.arr.Length)
            {
                bdz.arr[i] = bd.arr[i];   //O(1) 
            }
            Big_int subz = new Big_int((mid) + subt.arr.Length); //O(1)
            for (int i = 0; i < subt.arr.Length; i++) //O(subt.arr.Length)
            {
                subz.arr[i] = subt.arr[i]; //O(1)
            }
            Big_int sum = new Big_int(0); //O(1)
            ADD(bdz, subz, ref sum); //O(N)
            sum = remove_zeros(sum); //O(N)
            res = new Big_int(0); //O(1)
            ADD(sum, ac, ref res); //O(N)
            res = remove_zeros(res); //O(N)
            return res;//O(1)
        }
        //complixty of fun mult = O(subt.arr.Length)+O(bd.arr.Length)+3T(N/2)+O(mid)+O(x_str)+O(Y_str)+O(N)
        // in exact case all = N 
        //T(N) =3T(N/2)+O(N) By Master Case 1
        //  𝑁log 3-ε base2=N   
        // so case 1 
        // exact O(N^1.6)
        public static Big_int remove_zeros(Big_int mul)
        {
            int zeros = 0; //O(1)
            for (int i = 0; i < mul.arr.Length; i++) //O(mul.arr.Length)
            {
                if (mul.arr[i] == 0)
                {
                    zeros = i;//O(1)
                    continue;//O(1)
                }
                else
                {
                    break;//O(1)
                }
            }
            if (zeros == mul.arr.Length)
            {
                mul = new Big_int(1);//O(1)
                mul.arr[0] = 0;//O(1)
                return mul;//O(1)
            }
            else if (zeros == 0 && mul.arr[0] == 0 && mul.arr.Length > 1)
            {
                Big_int temp = new Big_int(mul.arr.Length - 1);//O(1)
                int count = 0;//O(1)
                for (int i = 1; i < mul.arr.Length; i++)//O(1)
                {
                    temp.arr[count] = mul.arr[i];//O(1)
                    count++;//O(1)
                }
                return temp;//O(1)
            }
            else
            {
                Big_int temp = new Big_int(mul.arr.Length - zeros);//O(1)
                int count = 0;//O(1)
                for (int i = zeros; i < mul.arr.Length; i++)//O(1)
                {
                    temp.arr[count] = mul.arr[i];//O(1)
                    count++;//O(1)
                }
                return temp;//O(1)
            }
        }
        //complixty of fun remove_zeros = O(mul.arr.Length) 
        // in exact case all = N 
        // total complixty  O(N) 

        public static Big_int div(Big_int a, Big_int b, ref Big_int q, ref Big_int r)
        {
            int great = greater(a, b);                          //O(N) 
            if (a.arr.Length < b.arr.Length || great == 2)      //O(1) 
            {
                q = new Big_int(1);                             //O(1)
                q.arr[0] = 0;                                   //O(1)
                r = a;                                          //O(1)
                return q;                                       //O(1)
            }
            Big_int twoB = new Big_int(1);                      //O(1)
            ADD(b, b, ref twoB);                                //O(N)     
            twoB = remove_zeros(twoB);                          //O(N) 
            div(a, twoB, ref q, ref r);                         // T(2*N)+O(N)
            Big_int twoQ = new Big_int(1);                      //O(1)
            ADD(q, q, ref twoQ);                                //O(N)  
            q = remove_zeros(twoQ);                             //O(N)   
            great = greater(r, b);                              //O(N)
            if (r.arr.Length < b.arr.Length || great == 2)      //O(1)
            {
                return q;                                       //O(1) 
            }
            else
            {
                Big_int one = new Big_int(1);                   //O(1) 
                one.arr[0] = 1;                                 //O(1) 
                Big_int Q_one = new Big_int(1);                 //O(1)
                ADD(q, one, ref Q_one);                         //O(N)   
                q = remove_zeros(Q_one);                        //O(N)   
                Big_int rSb = new Big_int(1);                   //O(1)  
                sub(r, b, ref rSb);                             //O(N) 
                r = remove_zeros(rSb);                          //O(N) 
                return q;                                       //O(1)   
            }
        }
        // T(N)=T(2N)+(N)
        // 
        //  

        private static int greater(Big_int a, Big_int b)
        {
            if (a.arr.Length > b.arr.Length)        //O(1)
            {
                return 1;                           //O(1)
            }
            else if (a.arr.Length < b.arr.Length)   //O(1)
            {
                return 2;                           //O(1)   
            }
            for (int i = 0; i < a.arr.Length; i++)   //O(N)
            {
                if (a.arr[i] > b.arr[i])             //O(1)
                {
                    return 1;                        //O(1)
                }
                else if (a.arr[i] < b.arr[i])        //O(1)
                {
                    return 2;                        //O(1)  
                }
            }
            return 0;                                //O(1)
        }

        //complixty of fun greater of best case = O(1) 
        //complixty of fun greater of worst case = O(N) 
        

        public static Big_int pow_mod(Big_int number, Big_int power, Big_int mod, ref Big_int res)
        {


            if (mod.arr.Length == 1 && mod.arr[0] == 1)           //O(1)
            {
                res = new Big_int(1);                            // O(1)     
                res.arr[0] = 0;                                 // O(1)       
                return res;                                     // O(1)     
            }
            if (power.arr.Length == 1 && power.arr[0] == 0)     // O(1)     
            {
                res = new Big_int(1);                           // O(1)     
                res.arr[0] = 1;                                 // O(1)     
                return res;                                     // O(1)     
            }
            if (power.arr.Length == 1 && power.arr[0] == 1)      // O(1)     
            {
                res = number;                                     // O(1)     
                res = remove_zeros(res);                          // O(N)     
                return res;                                       // O(1)     
            }

            if (power.arr[power.arr.Length - 1] % 2 == 0)         // O(1)    
            {
                Big_int new_power = new Big_int(1);               // O(1)    
                new_power.arr[0] = 2;                             // O(1)      
                Big_int rem = new Big_int(1);                     // O(1)    
                div(power, new_power, ref new_power, ref rem);    // O(N)     
                new_power = remove_zeros(new_power);              //O(N)
                Big_int value = new Big_int(1);                   //O(N)  


                pow_mod(number, new_power, mod, ref res);         //T(N/2)+N^1.6
                res = remove_zeros(res);                           //O(N)
                res = mult(res, res, ref value);                     //O(N^1.6) 
                res = remove_zeros(res);                               //O(N)

            }
            else
            {
                Big_int new_power = new Big_int(1);                      //O(1) 
                new_power.arr[0] = 2;                                    //O(1) 
                Big_int rem = new Big_int(1);                            //O(1)
                div(power, new_power, ref new_power, ref rem);           //O(N)
                new_power = remove_zeros(new_power);                      //O(N)
                Big_int value = new Big_int(1);                            //O(1)                     


                pow_mod(number, new_power, mod, ref res);                 //T(N/2)+N^1.6
                res = remove_zeros(res);                                   //O(N)  
                res = mult(res, res, ref value);                           //O(N^1.6)
                res = remove_zeros(res);                                   //O(N)
                mult(res, number, ref res);                                //O(N^1.6)  
                res = remove_zeros(res);                                   //O(N)

            }
            Big_int q = new Big_int(1);                                         //O(1)

            div(res, mod, ref q, ref res);                                      //O(N)

            res = remove_zeros(res);                                            //O(N)
            return res;                                                         //O(1)
        }
        // T(N/2)+N^1.6

    }
}
