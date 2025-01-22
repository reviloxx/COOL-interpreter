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
                   result <- true;
                   
                   out_string(" ----------------- Testing n: ");
                   out_intln(n);
                   
                   while j < n loop
                       {
                           out_string("Testing divisor j: ");
                           out_intln(j);
                           out_string("modulo result: ");
                           out_intln(math.modulo(n, j));
                           
                           if math.modulo(n, j) = 0 then
                               {
                                   out_stringln("Found divisor! Setting result to false");
                                   result <- false;
                               }
                           else 
                               {
                                   result;
                               };
                           j <- j + 1;
                       }
                   pool;
                   
                   out_string("Final result for n: ");
                   out_int(n);
                   out_string(" is: ");
                   if result then out_stringln("true") else out_stringln("false");
                   
                   result;
               }
            
        }
    };
    main() : SELF_TYPE {
        {
            math <- new Math;
            i <- 2;
            countUntil <- 10
            
            -- daweil auf 10 damit nich so viel unnötiger code
            out_string("We count until: ")
            out_intln(countUntil)
            
            while i < countUntil loop 
                {
                    if is_prime(i, math) then
                        {
                            out_string("is_prime = true for: ")
                            out_intln(i);
                        }
                    else
                        {
                            true;
                        };
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
            
        }
    };
};