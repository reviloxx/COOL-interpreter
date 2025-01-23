class Main inherits IO {
    is_prime(n : Int, math : Math) : Bool { 
        {
            if n < 2 then
                {
                    false;
                }
            else
               {
                   j <- 2;
                   isPrime <- true;                   
                   
                   while j < n loop
                       {                           
                           if isPrime = true then
                              {
                               if math.modulo(n, j) = 0  then
                                   {
                                       isPrime <- false;
                                    else 
                                   {
                                       isPrime <- true;
                                   }fi;
                               } else {
                               }fi;
                               
                               j <- j + 1;
                       }
                   pool;
                   
                   out_string("+%+%+%+%+%+%+%+%+%+ Finally, is '");
                   out_int(n);
                   out_string("' a primeNumber? ");
                   
                   out_string(" | Answer: ");
                   if isPrime then out_stringln("yes +%+%+%+%+%+%+%+%+") else out_stringln("no +%+%+%+%+%+%+%+%+")fi;
               }fi;
            
        }
    };
    main() : SELF_TYPE {
        {
            math <- new Math;
            i <- 2;
            countUntil <- 10
            out_string("We count until: ")
            out_intln(countUntil)
            
            while i <= countUntil loop 
                {
                    is_prime(i, math);
                    i <- i + 1;
                }
            pool;
            
            out_stringln("Bye, World!");
            self;
        }
    };
};

class Math {
    modulo(a : Int, b : Int) : Int {
        {
            if b = 0 then
                {
                    out_stringln("Error: Division by zero in modulo operation.");
                    0;
                }
            else
                (a - (a / b) * b)
            fi
        }
    };
};