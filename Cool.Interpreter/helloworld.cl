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
                   
                   out_string(" ---------------------------------- Testing n: ");
                   out_intln(n);
                   
                   while j < n loop
                       {
                           out_string("Testing divisor j: ");
                           out_intln(j);
                           out_string("math.modulo(n, j) is: ");
                           out_int(math.modulo(n, j));
                           out_string(" --> is math.modulo(n, j) = 0? --> ");
                           out_stringln(math.modulo(n, j) = 0);
                           -- out_string("right now, 'isPrime is:'");
                           -- out_stringln(isPrime);
                           
                           if isPrime = true then
                              {
                               -- out_stringln("in 'if isPrime = true'");
                               if math.modulo(n, j) = 0  then
                                   {
                                       -- out_stringln("++++ Found divisor! Setting isPrime to false ++++");
                                       isPrime <- false;
                                       -- out_string("right now, in if math.modulo(n, j) = 0, 'isPrime is:'");
                                       -- out_stringln(isPrime);
                                   }
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
            -- daweil auf 5 damit nich so viel unnötiger code
            out_string("We count until: ")
            out_intln(countUntil)
            
            while i <= countUntil loop 
                {
                    if is_prime(i, math) then
                        {
                            out_string("is_prime = true for: ")
                            out_intln(i);
                        }
                    else
                        {
                            true;
                        }fi;
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