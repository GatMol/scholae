-- DropForeignKey
ALTER TABLE "Libro" DROP CONSTRAINT "Libro_Materia_id_fkey";

-- DropForeignKey
ALTER TABLE "Libro" DROP CONSTRAINT "Libro_Utente_id_fkey";

-- AddForeignKey
ALTER TABLE "Libro" ADD FOREIGN KEY ("Materia_id") REFERENCES "Materia"("Nome") ON DELETE RESTRICT ON UPDATE CASCADE;

-- AddForeignKey
ALTER TABLE "Libro" ADD FOREIGN KEY ("Utente_id") REFERENCES "Utente"("Id") ON DELETE RESTRICT ON UPDATE CASCADE;
