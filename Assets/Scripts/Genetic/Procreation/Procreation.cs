using System.Collections.Generic;

public interface Procreation<G> where G : Gene, new()
{
    List<DNA<G>> BuildNextGeneration(List<DNA<G>> fittest, int generationSize, int survivorKeepPercentage = 30, int mutationChance = 10, int mutationRate = 30, bool autoProcreation = false);
}