
# Projeyi Çalıştırma Rehberi

Bu rehber, **AR_Project**'i çalıştırmak için gerekli adımları içerir. Proje, Unity kullanılarak geliştirilmiştir ve Vuforia Augmented Reality paketi gerektirir.

---

## 1. Gerekli Araçlar ve Yazılımlar

- **Unity Editor**: Unity 6000.0.25f1 sürümünü [buradan](https://unity.com/releases/editor/archive) indirip yükleyin.  
- **Vuforia Engine**: Vuforia Unity paketini indirmeniz gerekecek. Aşağıdaki adımları izleyin.

---

## 2. Projeyi Klonlama

1. GitHub deposunu klonlayın:
   ```bash
   git clone https://github.com/username/AR_Project.git
   ```
2. **`/Project`** klasörüne gidin.

---

## 3. Vuforia Engine Kurulumu

1. Vuforia'nın resmi sitesine [buraya tıklayarak](https://developer.vuforia.com/downloads/sdk) erişin.  
2. Unity için Vuforia Engine paketini indirin (Unity Package dosyası).  
3. İndirdiğiniz paketi Unity'e eklemek için aşağıdaki adımları takip edin:
   - Unity'de projeyi açtıktan sonra **`Assets` > `Import Package` > `Custom Package`** yolunu izleyin.
   - İndirdiğiniz dosyayı seçin ve yükleme işlemini tamamlayın.

---

## 4. Projeyi Unity'de Açma

1. Unity Hub'ı açın.  
2. **`Add Project`** butonuna tıklayın.  
3. Klonladığınız dizindeki **`/Project`** klasörünü seçin ve projeyi ekleyin.  
4. Projeyi Unity'de açın.

---

## 5. Sahne Kontrolleri ve Güncellemeler

1. Proje içindeki sahneleri inceleyin.  
2. **`EventSystem`**'lerin sahnelerde var olduğundan emin olun. Eksikse şu adımları izleyin:
   - **`Hierarchy`** penceresinde sağ tıklayın ve **`UI` > `Event System`** seçeneğini kullanarak yeni bir **`EventSystem`** ekleyin.
3. Sahne dosyalarındaki eksik bağımlılıkları kontrol edin ve gerekli düzenlemeleri yapın.

---

## 6. Uygulamayı Çalıştırma

1. Projeyi çalıştırmak için Unity'de **`Play`** butonuna tıklayın.  
2. Artırılmış gerçeklik tetikleyicisini (marker'ı) kameraya gösterin.  
3. Uygulamanın düzgün çalıştığından emin olun.

---

Bu rehber, projeyi çalıştırırken karşılaşabileceğiniz olası sorunları çözmek için hazırlanmıştır. Daha fazla yardım için [İletişim](mailto:yucetalha00@gmail.com) bölümüne başvurabilirsiniz.
