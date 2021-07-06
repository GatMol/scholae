/*
  Warnings:

  - The primary key for the `LibroSalvato` table will be changed. If it partially fails, the table could be left without primary key constraint.
  - You are about to drop the column `Libro` on the `LibroSalvato` table. All the data in the column will be lost.
  - You are about to drop the column `Utente` on the `LibroSalvato` table. All the data in the column will be lost.
  - Added the required column `Libro_id` to the `LibroSalvato` table without a default value. This is not possible if the table is not empty.
  - Added the required column `Utente_id` to the `LibroSalvato` table without a default value. This is not possible if the table is not empty.

*/
-- DropForeignKey
ALTER TABLE "LibroSalvato" DROP CONSTRAINT "LibroSalvato_Libro_fkey";

-- DropForeignKey
ALTER TABLE "LibroSalvato" DROP CONSTRAINT "LibroSalvato_Utente_fkey";

-- AlterTable
ALTER TABLE "LibroSalvato" DROP CONSTRAINT "LibroSalvato_pkey",
DROP COLUMN "Libro",
DROP COLUMN "Utente",
ADD COLUMN     "Libro_id" BIGINT NOT NULL,
ADD COLUMN     "Utente_id" BIGINT NOT NULL,
ADD PRIMARY KEY ("Libro_id", "Utente_id");

-- AddForeignKey
ALTER TABLE "LibroSalvato" ADD FOREIGN KEY ("Libro_id") REFERENCES "Libro"("Id") ON DELETE CASCADE ON UPDATE CASCADE;

-- AddForeignKey
ALTER TABLE "LibroSalvato" ADD FOREIGN KEY ("Utente_id") REFERENCES "Utente"("Id") ON DELETE CASCADE ON UPDATE CASCADE;
