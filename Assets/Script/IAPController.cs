using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class IAPController : MonoBehaviour, IStoreListener
{
    private static  IAPController Instance { set; get; }
    private static IStoreController m_StoreController;          // The Unity Purchasing system.
    private static IExtensionProvider m_StoreExtensionProvider; // The store-specific Purchasing subsystems.

    // specific mapping to Unity Purchasing's AddProduct, below.
    public static string productAbone1 = "aboneol1";
    public static string productAbone2 = "abone2ol";
    public static string productAbone3 = "aboneol3";
    private static string productAbone4 = "abone4ol";
    

    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        if (m_StoreController == null)
        {
            InitializePurchasing();
        }
    }
  

    public void InitializePurchasing()
    {

        if (IsInitialized())
        {

            return;
        }

        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        builder.AddProduct(productAbone1, ProductType.Subscription);
        builder.AddProduct(productAbone2, ProductType.Subscription);
        builder.AddProduct(productAbone3, ProductType.Subscription);
        builder.AddProduct(productAbone4, ProductType.Subscription);
        UnityPurchasing.Initialize(this, builder);
    }
    private bool IsInitialized()
    {
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }
    
    public void BuySubscription1()
    {
        BuyProductID(productAbone1);
    }
    public void BuySubscription2()
    {
        BuyProductID(productAbone2);
    }
    public void BuySubscription3()
    {
        BuyProductID(productAbone3);
    }
    public void BuySubscription4()
    {
        BuyProductID(productAbone4);
    }

    void BuyProductID(string productId)
    {
        if (IsInitialized())
        {
            Product product = m_StoreController.products.WithID(productId);

            if (product != null && product.availableToPurchase)
            {
                Debug.Log(string.Format("Purchasing '", product.definition.id));
                m_StoreController.InitiatePurchase(product);
            }
            else
            {
                Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
            }
        }
        else
        {
            Debug.Log("BuyProductID FAIL. Not initialized.");
        }
    }

    public void RestorePurchases()
    {
        if (!IsInitialized())
        {
            Debug.Log("RestorePurchases FAIL. Not initialized.");
            return;
        }
        if (Application.platform == RuntimePlatform.IPhonePlayer ||
            Application.platform == RuntimePlatform.OSXPlayer)
        {
            Debug.Log("RestorePurchases started ...");
            var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
            apple.RestoreTransactions((result) =>
            {
                Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
            });
        }
        else
        {
            Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
        }
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
       // Purchasing has succeeded initializing. Collect our Purchasing references.
        Debug.Log("OnInitialized: PASS");
        // Overall Purchasing system, configured with products for this application.
        m_StoreController = controller;
        // Store specific subsystem, for accessing device-specific store features.
        m_StoreExtensionProvider = extensions;
        //This can be called anytime after initialization
        //And it should probably be limited to Google Play and not just Android
#if !UNITY_EDITOR
        foreach (Product p in controller.products.all)
        {
            // Refering to the extra GooglePurchaseData class provided
            GooglePurchaseData data = new GooglePurchaseData(p.receipt);
            if (p.hasReceipt)
            {
                // Allows you to easily refer to data from the receipt for the subscription,
                // if AutoRenewing is true, then their subscrition is active.
                Debug.Log("autoRenewing: "+data.json.autoRenewing);
                if (data.json.autoRenewing == "true")
                {
                    Data.isSubscriber = true;
                    Badge.init();
                    db_connect.ClosePaywall();
                }
                Debug.Log(data.json.orderId);
                Debug.Log(data.json.packageName);
                Debug.Log(data.json.productId);
                Debug.Log(data.json.purchaseTime);
                Debug.Log(data.json.purchaseState);
                Debug.Log(data.json.purchaseToken);
            }
        }
#endif

    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        if (String.Equals(args.purchasedProduct.definition.id, productAbone1, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("abone 1 satın alındı"));
        }
        // Or ... a non-consumable product has been purchased by this user.
        else if (String.Equals(args.purchasedProduct.definition.id, productAbone2, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("abone 2 satın alındı"));
        }
        else if (String.Equals(args.purchasedProduct.definition.id, productAbone3, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("abone 3 satın alındı"));
        }
        else if (String.Equals(args.purchasedProduct.definition.id, productAbone4, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("abone 4 satın alındı"));
        }

        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {

        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }



   
  
 
}
