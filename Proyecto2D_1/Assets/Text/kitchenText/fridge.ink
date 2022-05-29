VAR objetoFridge = 0
-> Inicio

=== Inicio ===
¡Es la nevera!
{objetoFridge == 0: -> noTieneNada}
{objetoFridge > 0: -> tienesAlgo}
->END


=== noTieneNada ===
Veamos que hay por aquí...
***Ese pescado tan feo
    Coges el pez. Parece que te está mirando...
    ~objetoFridge = 1
    ->END
***El bloque de hielo raro del congelador
    Coges el bloque de hielo. ¡QUE FRIO!
    Parece que tiene algo en su interior...
    ~objetoFridge = 2
    ->END
->END

=== tienesAlgo ===
Ya tienes las manos ocupadas.
¿Dejar lo que llevas?
***Dejar
    Lo pones donde estaba.
    ~objetoFridge = 0
    ->END
***Mejor me lo quedo
    Cierras la nevera.
    ->END
->END